using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProStats : MonoBehaviour
{
    public float Dam;
    public float Speed = 100f;
    public float Pen;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        Dam = 5f;
        Speed = 100f;
        Pen = 50f;
        angle = this.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
