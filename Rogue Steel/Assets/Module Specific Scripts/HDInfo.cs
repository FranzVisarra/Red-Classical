using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HDInfo : MonoBehaviour
{
    public float curRotSp;
    public float rotateSpeed;
    void Start()
    {
        curRotSp = rotateSpeed;
    }
    public void Destroyed()
    {
        curRotSp = 0;
    }
}
