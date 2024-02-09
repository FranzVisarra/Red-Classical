using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionList
{
    public string type;
    public Vector2 pos;
    // Start is called before the first frame update
    public DetectionList(string type, Vector2 pos)
    {
        this.type = type;
        this.pos = pos;
    }
}

public class EnTnkStats : MonoBehaviour
{
    public List<DetectionList> dlist;
    public string AiState;//patrol, investigate, search, attack
    public string DetState;//nothing, known, heard, seen
    public Vector2 TestNoiseDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        dlist = new List<DetectionList>();
        AiState = "Patrol";
        DetState = "None";
    }

    // Update is called once per frame
    void Update()
    {
        AiStateMachine();
        DetStateMachine();
    }

    private void DetStateMachine()
    {
        //throw new NotImplementedException();
    }

    private void AiStateMachine()
    {
        if (AiState == "Patrol")
        {
            if (dlist.Exists(DetectionList => DetectionList.type=="Sound"))
            {
                DetState = "Sound Heard";
                AiState = "Investigate";
            }
        }
        else if (AiState == "Investigate")
        {
            if (dlist.Exists(DetectionList => DetectionList.type == "Sight"))
            {
                DetState = "Seen";
                AiState = "Attack";
            }
        }
        else if (AiState == "Attack")
        {
            if (!dlist.Exists(DetectionList => DetectionList.type == "Sight"))
            {
                DetState = "Known";
                AiState = "Search";
            }
        }
        else if (AiState == "Search")
        {

        }
    }

    public void soundHeard(Vector3 sSource)
    {
        TestNoiseDirection.x = sSource.x;
        TestNoiseDirection.y = sSource.y;
        dlist.Add(new DetectionList("Sound",TestNoiseDirection));
    }
}
