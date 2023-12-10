using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProStats : MonoBehaviour
{
    public float Dam;
    public float Speed = 100f;
    public float Pen;
    public Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        Dam = 5f;
        Speed = 100f;
        Pen = 50f;
        //startPos = new Vector2(transform.position.x,transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
