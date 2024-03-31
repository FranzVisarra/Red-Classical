using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInfo : MonoBehaviour, ModSpecInfo
{
    public void Destroyed()
    {

    }
    public void Test()
    {
        Debug.Log(this.GetType().ToString());
    }
}
