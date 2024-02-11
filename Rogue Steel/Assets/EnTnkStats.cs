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
    public int countSight;
    public Vector2 targPos;
    
    // Start is called before the first frame update
    void Start()
    {
        dlist = new List<DetectionList>();
        dlist.Add(new DetectionList("Player", new Vector2(0, 0)));//index 0 is players last known position
        AiState = "Patrol";
        DetState = "None";
    }

    // Update is called once per frame
    void Update()
    {
        DetListHand();
        AiStateMachine();
    }

    private void DetListHand()
    {
        //increment time
        for (int i = 0; i < dlist.Count; i++)
        {
            dlist[i].time++;
        }
        //clear list
        for (int i = dlist.Count - 1; i >= 0; i--)
        {
            if (dlist[i].type == "Sound")
            {
                if (dlist[i].time > 10*50)//test value 50 frames * 10 seconds so sounds remembered for 10 seconds
                {
                    dlist.RemoveAt(i);
                }
            }
            else if (dlist[i].type == "Sight")
            {
                if (dlist[i].time > 10 * 50)//test value 50 frames * 10 seconds so sights remembered for 10 seconds
                {
                    dlist.RemoveAt(i);
                }
            }
            else if (dlist[i].type == "Hit")
            {
                if (dlist[i].time > 10 * 50)//test value 50 frames * 10 seconds so hits remembered for 10 seconds
                {
                    dlist.RemoveAt(i);
                }
            }
            else if (dlist[i].type == "Known")
            {
                if (dlist[i].time > 20 * 50)//test value 50 frames * 10 seconds so player remembered for 20 seconds
                {
                    DetState = "None";
                }
            }
        }
        //detection states
        if (countListByName("Sight") >= 50)//if more than 50 sight logs
        {
            DetState = "Seen";
        }
        else if (dlist.Exists(DetectionList => DetectionList.type == "Sound"))// if detection list has sound cues
        {
            DetState = "Heard";
        }
        else if (dlist.Count>0)//if detection list has more than one item
        {
            DetState = "Known";
        }
    }

    private void AiStateMachine()//not done by a long shot
    {
        if (AiState == "Patrol")
        {
            if (DetState == "Heard" || DetState == "Known")
            {
                AiState = "Investigate";
            }
            else if (DetState == "Seen")
            {
                AiState = "Attack";
            }
        }
        else if (AiState == "Investigate")
        {
            if (DetState == "None")
            {
                AiState = "Patrol";
            }
            else if (DetState == "Seen")
            {
                AiState = "Attack";
            }
        }
        else if (AiState == "Attack")
        {
            if (DetState == "None")
            {
                AiState = "Patrol";
            }
            else if (DetState == "Heard" || DetState == "Known")
            {
                AiState = "Investigate";
            }
        }
    }

    public void soundHeard(Vector3 sSource)
    {
        Direction.x = sSource.x;
        Direction.y = sSource.y;
        detectedPlayer(Direction);
        dlist.Add(new DetectionList("Sound",Direction));
        if (countListByName("Sight") >= 5)
        {
            dlist.RemoveAt(returnOldestOfName("Sound"));
        }
    }
    //need to limit sight references
    public void EnemySeen(Vector3 sSource)
    {
        Direction.x = sSource.x;
        Direction.y = sSource.y;
        dlist.Add(new DetectionList("Seen", Direction));
        detectedPlayer(Direction);
        if (countListByName("Sight") >= 50)
        {
            dlist.RemoveAt(returnOldestOfName("Sight"));
        }
    }

    public void hitByEnemy(Vector3 sSource)
    {
        Direction.x = sSource.x;
        Direction.y = sSource.y;
        detectedPlayer(Direction);
        dlist.Add(new DetectionList("Hit", Direction));
    }

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
        int time = 0;
        int index = 0;
        for (int i = 0;i< dlist.Count; i++)
        {
            if (dlist[i].type == name)
            {
                if (dlist[i].time > time)
                {
                    index = i;
                }
            }
        }
        return index;
    }
    public void detectedPlayer(Vector2 pos)
    {
        dlist[0].time = 0;
        dlist[0].pos = pos;
    }
}
