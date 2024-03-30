using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewInfo : MonoBehaviour
{
    public GameObject Par;
    public GameObject Engine;
    public GameObject Cannon;
    public AllTnkStats stats;
    public string CrewType;
    public int inModule;
    public float timer;
    
    //TODO make a module that keeps extra crew and make it replenish
    void Start()
    {
        Par = this.transform.parent.gameObject;
        Engine = Par.transform.Find("Engine(Clone)").gameObject;
        stats = Par.GetComponent<AllTnkStats>();
        switch (CrewType)
        {
            case "Driver":
                stats.driverStatus = true;
                break;
            case "Gunner":
                stats.gunnerStatus = true;
                break;
            case "Loader":
                stats.loaderStatus = true;
                break;
            case "Reserve":
                stats.stats["Reserve"] += inModule;
                break;
        }
    }
    public void Destroyed()
    {
        switch(CrewType)
        {
            case "Driver":
                if (stats.stats["Reserve"] <= 0)
                {
                    stats.driverStatus = false;
                }
                else
                {
                    stats.stats["Reserve"]--;
                }
                if (stats.gunnerStatus && stats.loaderStatus && stats.stats["Reserve"]<=0)
                {
                    //trigger death
                }
                break;
            case "Gunner":
                if (stats.stats["Reserve"] <= 0)
                {
                    stats.gunnerStatus = false;
                }
                else
                {
                    stats.stats["Reserve"]--;
                }
                if (stats.driverStatus && stats.loaderStatus && stats.stats["Reserve"] <= 0)
                {
                    //trigger death
                }
                break;
            case "Loader":
                if (stats.stats["Reserve"] <= 0)
                {
                    stats.loaderStatus = false;
                }
                else
                {
                    stats.stats["Reserve"]--;
                }
                if (stats.gunnerStatus && stats.driverStatus && stats.stats["Reserve"] <= 0)
                {
                    //trigger death
                }
                break;
            case "Reserve":
                stats.stats["Reserve"] -= inModule;
                break;
        }
    }
}
