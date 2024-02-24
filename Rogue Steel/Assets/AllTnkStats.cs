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
    void Awake()
    {
        stats = new Dictionary<string, float>();
        storage = new List<StoredAmmo>();
        stats.Add("Speed", 0);
        stats.Add("Fuel", 0);
    }
    public int CalculateAmmoVolume(string caliber)
    {
        curAmount = 0;
        foreach (var ammo in storage)
        {
            if (caliber == ammo.caliber)
            {
                curAmount += ammo.amount;
            }
        }
        return curAmount;
    }

    //TODO come up with a better algorythm for removing ammo
    public void removeExcessAmmo(string caliber)
    {
        int excess = CalculateAmmoVolume(caliber)-(int)stats[caliber];
        for (int i = storage.Count - 1; i >= 0; i--)
        {
            storage[i].amount -= excess;
            if (storage[i].amount <= 0)
            {
                storage.RemoveAt(i);
            }
        }
    }
    public void AddAmmo(string name, string caliber, int amount)
    {
        if (amount + CalculateAmmoVolume(caliber) > stats[caliber])
        {
            storage.Add(new StoredAmmo(name, caliber, amount));
        }
        else
        {
            int excess = amount + CalculateAmmoVolume(caliber) - (int)stats[caliber];
            storage.Add(new StoredAmmo(name, caliber, amount-excess));
            //TODO add excess to pickup inventory
        }
    }
}
