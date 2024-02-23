using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StoredAmmo
{
    public string name;
    public string caliber;
    public int amount;

    public StoredAmmo(string name, string caliber, int amount)
    {
        this.name = name;
        this.caliber = caliber;
        this.amount = amount;
    }
}

public class AllTnkStats : MonoBehaviour
{
    public Dictionary<string, float> stats;
    public List<StoredAmmo> storage;
    public int curAmount;
    // Start is called before the first frame update
    void Start()
    {
        storage = new List<StoredAmmo>();
        stats.Add("Speed", 0);
        stats.Add("Fuel", 0);
    }
    public int CalculateAmmoVolume()
    {
        curAmount = 0;
        foreach (var ammo in storage)
        {
            curAmount += ammo.amount;
        }
        return curAmount;
    }
    public void removeExcessAmmo(string caliber)
    {
        int excess = CalculateAmmoVolume()-(int)stats[caliber];
        for (int i = storage.Count - 1; i >= 0; i--)
        {
            storage[i].amount -= excess;
            if (storage[i].amount <= 0)
            {
                storage.RemoveAt(i);
            }
        }
    }
}
