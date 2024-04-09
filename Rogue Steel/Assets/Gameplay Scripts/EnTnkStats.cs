using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class DetectionList
{
    public string type;
    public Vector2 pos;
    public int time;
    // Start is called before the first frame update
    public DetectionList(string type, Vector2 pos)
    {
        this.type = type;
        time = 0;
        this.pos = pos;
    }
}

public class EnTnkStats : MonoBehaviour
{
    public List<DetectionList> dlist;
    public string AiState;//patrol, investigate, search, attack
    public string DetState;//nothing, known, heard, seen
    public Vector2 Direction;
    public GameObject En;
    
    // Start is called before the first frame update
    void Start()
    {
        dlist = new List<DetectionList>();
        dlist.Add(new DetectionList("Player", new Vector2(En.transform.position.x, En.transform.position.y)));//index 0 is players last known position
        AiState = "Patrol";
        DetState = "None";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " FixedUpdate Start");
        DetListHand();
        //Debug.Log(this.GetType().ToString() + " FixedUpdate End");
    }
    private void Update()
    {
        //Debug.Log(this.GetType().ToString() + " Update Start");
        AiStateMachine();
        //Debug.Log(this.GetType().ToString() + " Update End");
    }

    private void DetListHand()
    {
        int SoundCount = 0;
        int SightCount = 0;
        int HitCount = 0;
        //increment time
        for (int i = 0; i < dlist.Count; i++)
        {
            dlist[i].time++;
        }
        //clear list... order doesnt matter because type is unique
        for (int i = dlist.Count - 1; i >= 0; i--)//iterate backwards for better removal
        {
            switch (dlist[i].type)
            {
                case "Sound":
                    SoundCount += returnValid(i, SoundCount, 10, 10);
                    break;
                case "Sight":
                    SightCount += returnValid(i, SightCount, 100, 10);
                    break;
                case "Hit":
                    HitCount += returnValid(i, HitCount, 1, 10);
                    break;
            }
            /*
            if (dlist[i].type == "Sound")
            {
                SoundCount += returnValid(i, SoundCount, 10, 10);
            }
            else if (dlist[i].type == "Sight")
            {
                SightCount += returnValid(i, SightCount, 100, 10);
            }
            else if (dlist[i].type == "Hit")
            {
                HitCount += returnValid(i, HitCount, 1, 10);
            }
            */
        }
        //----------detection states----------//
        //sight takes priority
        if (SightCount >= 50)//if more than 50 sight logs
        {
            dlist[0].pos = dlist[returnOldestOfName("Sight")].pos;
            dlist[0].time = 0;
            DetState = "Seen";
        }
        /*
        else if (dlist.Exists(DetectionList => DetectionList.type == "Sound"))// if detection list has sound cues
        {
            DetState = "Heard";
        }
        */
        //sound is next
        else if (SoundCount >= 1)// if detection list has sound cues
        {
            dlist[0].pos = dlist[returnOldestOfName("Sound")].pos;
            dlist[0].time = 0;
            DetState = "Heard";
        }
        else if (dlist.Count == 1 || dlist[0].time < 20 * 50)//if detection list has only one item, which is last known player position
        {
            DetState = "Known";
        }
        else /*if (dlist[0].time >= 20 * 50)*///test value 50 frames * 10 seconds so player remembered for 20 seconds
        {
            //reset first index
            dlist[0].pos = new Vector2(0, 0);
            DetState = "None";
        }
        //----------detection states----------//
    }
    /* this method is genious.
     * saves similar code
     * reduces complexity because the limit was originally in the public methods that add an item but those require an additional 2 for loops
     */
    private int returnValid(int index, int count, int limit, int expire)//checks if index is still valid then returns 1 if yes
    {
        if (count >= limit || dlist[index].time >= expire * 50)//check if count is greater than limit and test value 50 frames * expire seconds
        {
            dlist.RemoveAt(index);//remove at index then return 0
        }
        else//increment
        {
            return 1;
        }
        return 0;
    }
    private void AiStateMachine()//not done by a long shot
    {
        switch (AiState)
        {
            case "Patrol":
                /* TODO
                * -Follow patrol beacon
                * -turn turret to check
                */
                if (DetState == "Heard" || DetState == "Known")
                {
                    AiState = "Investigate";
                }
                else if (DetState == "Seen")
                {
                    AiState = "Attack";
                }
                break;
            case "Investigate":
                /* TODO
                * -Follow targpos
                * -if at targpos, rotate turret to check for player
                */
                if (DetState == "None")
                {
                    AiState = "Patrol";
                }
                else if (DetState == "Seen")
                {
                    AiState = "Attack";
                }
                break;
            case "Attack":
                /* TODO
                * -Chase Maybe? Defend?
                * -turret snaps to targpos
                */
                if (DetState == "None")
                {
                    AiState = "Patrol";
                }
                else if (DetState == "Heard" || DetState == "Known")
                {
                    AiState = "Investigate";
                }
                break;
        }
    }

    public void soundHeard(Vector3 sSource)
    {
        Direction.x = sSource.x;
        Direction.y = sSource.y;
        detectedPlayer(Direction);
        dlist.Add(new DetectionList("Sound",Direction));
    }

    public void enemySeen(Vector3 sSource)
    {
        Direction.x = sSource.x;
        Direction.y = sSource.y;
        dlist.Add(new DetectionList("Sight", Direction));
        detectedPlayer(Direction);
        /*
         * this might be re added because the for loop in the update method may
         * have too many items in it when it goes through it
        if (countListByName("Sight") >= 50)
        {
            dlist.RemoveAt(returnOldestOfName("Sight"));
        }
        */
    }

    public void hitByEnemy(Vector3 sSource)
    {
        Direction.x = sSource.x;
        Direction.y = sSource.y;
        detectedPlayer(Direction);
        dlist.Add(new DetectionList("Hit", Direction));
    }
    //we could totally move this to the for loop that counts
    public int countListByName(string name)
    {
        int amount = 0;
        for (int i = 0; i < dlist.Count; i++)
        {
            if (dlist[i].type == name)
            {
                amount++;
            }
        }
        return amount;
    }
    /*
    public void clearListByName(string name)
    {
        for (int i = dlist.Count - 1; i >= 0; i--)
        {
            if (dlist[i].type == name)
            {
                dlist.RemoveAt(i);
            }
        }
    }
    */
    public int returnOldestOfName(string name)
    {
        for (int i = 0; i < dlist.Count; i++)//oldest is earliest in the list
        {
            if (dlist[i].type == name)
            {
                return i;
            }
        }
        return 0;
    }
    public int returnYoungestOfName(string name)
    {
        for (int i = dlist.Count-1; i >= 0; i--)//iterate backwards...top is newest
        {
            if (dlist[i].type == name)
            {
                return i;
            }
        }
        return 0;
    }
    public void detectedPlayer(Vector2 pos)
    {
        dlist[0].time = 0;
        dlist[0].pos = pos;
    }
}
