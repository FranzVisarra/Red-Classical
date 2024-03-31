using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDInfo : MonoBehaviour, ModSpecInfo
{
    public float curRotSp;
    public float rotateSpeed;
    void Start()
    {
        curRotSp = rotateSpeed;
    }
    public void Test()
    {
        Debug.Log(this.GetType().ToString());
    }

    public void Destroyed()
    {
        curRotSp = 0;
    }
}
