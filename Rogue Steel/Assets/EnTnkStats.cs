using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnTnkStats : MonoBehaviour
{
    public string AiState;//patrol, investigate, search, attack
    public string DetState;//nothing, known, heard, seen
    public Vector2 TestNoiseDirection;
    // Start is called before the first frame update
    void Start()
    {
        AiState = "Patrol";
        DetState = "None";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void soundHeard(Vector3 sSource)
    {
        TestNoiseDirection.x = sSource.x;
        TestNoiseDirection.y = sSource.y;
        DetState = "Sound Heard";
        AiState = "Investigate";
    }
}
