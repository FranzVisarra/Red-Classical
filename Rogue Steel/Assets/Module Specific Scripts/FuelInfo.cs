using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelInfo : MonoBehaviour, ModSpecInfo
{
    public float maxFuel;
    public GameObject Par;
    public AllTnkStats stats;
    public ModuleInfo Module;
    public Vector3 thisPosition;
    public Vector3 nextPosition;
    void Awake()
    {
        maxFuel = 0;
        Par = this.transform.parent.gameObject;
        stats = Par.GetComponent<AllTnkStats>();
    }
    //invoked on spawn
    public void AddMaxFuelFull()
    {
        stats.SetMaxFuel(stats.stats["MaxFuel"] +maxFuel);
        stats.AddFuel(maxFuel);
    }
    //invoked when repaired
    public void AddMaxFuel()
    {
        stats.SetMaxFuel(stats.stats["MaxFuel"] + maxFuel);
    }
    public void Test()
    {
        Debug.Log(this.GetType().ToString());
    }

    public void Destroyed()
    {
        /*max fuel of this object - difference between total max and current fuel
         * |-------/ |
         *      |  / |
         *      
         * |    |    |
         */
        if (maxFuel > stats.stats["MaxFuel"] - stats.stats["Fuel"])//check if max is greater than the fuel removed
        {
            stats.RemoveFuel(maxFuel - (stats.stats["MaxFuel"] - stats.stats["Fuel"]));//subtract current fuel by the max minus the removed fuel
        }
        stats.RemoveFuel(stats.stats["Fuel"]);
        stats.SetMaxFuel(stats.stats["MaxFuel"] - maxFuel);
    }
}
