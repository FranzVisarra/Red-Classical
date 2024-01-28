using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProStats : MonoBehaviour
{
    public float Dam;
    public float Velocity;
    public float Pen;
    public Vector2 startPos;
    // Start is called before the first frame update
    void Awake()
    {
        Dam = 5f;
        Velocity = 10f;
        Pen = 0.5f;
        //startPos = new Vector2(transform.position.x,transform.position.y);
    }
    /*
    public void setStartPos(Vector2 i)
    {
        startPos = i;
    }
    */
}
