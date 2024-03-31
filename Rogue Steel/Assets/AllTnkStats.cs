using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public string tnkName;

    public GameObject mcns;
    public UIHandling UIH;
    public GameObject ItemDrop;

    public bool driverStatus;
    public bool gunnerStatus;
    public bool loaderStatus;
    // Start is called before the first frame update
    void Awake()
    {
        UIH = mcns.GetComponent<UIHandling>();
        stats = new Dictionary<string, float>();
        storage = new List<StoredAmmo>();
        stats.Add("Speed", 0);
        stats.Add("FuelBurn", 0);
        stats.Add("MaxFuel", 0);
        stats.Add("Fuel", 0);
        stats.Add("Reserve", 0);
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
    
    //invoked when ammo storage destroyed
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
    //invoked when ammo picked up or returned
    public void AddAmmo(string name, string caliber, int amount)
    {
        if (amount + CalculateAmmoVolume(caliber) <= stats[caliber])
        {
            switch (CheckAmmoInStorageByName(name))
            {
                case true:
                    IncrementAmmoByName(name, amount);
                    break;
                case false:
                    storage.Add(new StoredAmmo(name, caliber, amount));
                    break;
            }
        }
        else
        {
            int excess = amount + CalculateAmmoVolume(caliber) - (int)stats[caliber];
            switch (CheckAmmoInStorageByName(name))
            {
                case true:
                    IncrementAmmoByName(name, amount-excess);
                    break;
                case false:
                    storage.Add(new StoredAmmo(name, caliber, amount - excess));
                    //TODO add excess to pickup inventory
                    break;
            }
        }
    }

    //invoked when loading ammo to a cannon. check not made here.
    //TODO i really should
    public void RemoveAmmo(string name, int amount)
    {
        foreach (var ammo in storage)
        {
            if (name == ammo.name)
            {
                ammo.amount -= amount;
            }
        }
    }

    public bool CheckAmmoInStorageByName(string name)
    {
        bool ammoNameExists = false;
        //check if ammo type exists
        foreach (var ammo in storage)
        {
            //check if name matches
            if (ammo.name == name)
            {
                ammoNameExists = true;
            }
        }
        return ammoNameExists;
    }

    public bool CheckAmmoExistsInStorageByName(string name)
    {
        bool ammoExists = false;
        //check if ammo type exists
        foreach (var ammo in storage)
        {
            //check if name matches
            if (ammo.name == name)
            {
                if (ammo.amount > 0)
                {
                    ammoExists = true;
                }
            }
        }
        return ammoExists;
    }

    //invoked if guaranteed that ammo type is in there
    public void IncrementAmmoByName(string name, int amount)
    {
        foreach (var ammo in storage)
        {
            if (ammo.name == name)
            {
                ammo.amount += amount;
            }
        }
    }
    public int CountAmmoCalibers(string caliber)
    {
        int count = 0;
        foreach(var ammo in storage)
        {
            if (ammo.caliber == caliber)
            {
                count++;
            }
        }
        return count;
    }
    public void SetMaxFuel(float fuel)
    {
        stats["MaxFuel"] = fuel;
        UIH.setMaxFuel(stats["MaxFuel"]);

    }
    //invoked when refueling
    public void AddFuel(float fuel)
    {
        if (stats["Fuel"]+fuel <= stats["MaxFuel"])
        {
            stats["Fuel"] += fuel;
        }
        else
        {
            stats["Fuel"] = stats["MaxFuel"];
        }
        UIH.setFuel(stats["Fuel"]);
    }
    //invoked when moving
    public void BurnFuel(float dist)
    {
        stats["Fuel"] -= stats["FuelBurn"]*Time.deltaTime*dist;
        if(tnkName == "Player")
        {
            UIH.setFuel(stats["Fuel"]);
        }
    }
    //invoked when fuel tank destroyed
    public void RemoveFuel(float fuel)
    {
        if (stats["Fuel"] >= 0)
        {
            stats["Fuel"] -= fuel;
        }
        if (tnkName == "Player")
        {
            UIH.setFuel(stats["Fuel"]);
            UIH.setMaxFuel(stats["MaxFuel"]);
        }
    }
    //invoked when tank is out of crew
    public void Destroyed()
    {
        switch (tnkName)
        {
            case "Player":
                //TODO gameOver
                break;
            case "Light Tank":
                UIH.credits += 100;
                Instantiate(ItemDrop,this.transform.position,this.transform.rotation);
                //TODO do loot pool thing
                Destroy(this.transform.parent.gameObject);
                break;
        }
    }
}
