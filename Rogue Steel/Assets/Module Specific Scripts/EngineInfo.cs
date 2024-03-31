using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EngineInfo : MonoBehaviour, ModSpecInfo
{
    public float speed;//speed increase
    public float fuelEfficiency;
    public GameObject Par;
    public AllTnkStats stats;
    // Start is called before the first frame update
    void Awake()
    {
        Par = this.transform.parent.gameObject;
        stats = Par.GetComponent<AllTnkStats>();
    }

    public void AddStats()
    {
        stats.stats["Speed"] += speed;
        stats.stats["FuelBurn"] += fuelEfficiency;
    }
    public void Test()
    {
        Debug.Log(this.GetType().ToString());
    }

    public void Destroyed()
    {
        stats.stats["Speed"] -= speed;
        stats.stats["FuelBurn"] -= fuelEfficiency;
    }
}
