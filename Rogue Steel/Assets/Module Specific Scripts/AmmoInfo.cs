using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//will handle max amount and destruction of ammo
public class AmmoInfo : MonoBehaviour, ModSpecInfo
{
    public int maxAmount;
    public string caliber;
    public GameObject Par;
    public AllTnkStats stats;
    public void setStats(int maxAmount, string caliber)
    {
        this.maxAmount = maxAmount;
        this.caliber = caliber;
    }
    public void Awake()
    {
        Par = this.transform.parent.gameObject;
        stats = Par.GetComponent<AllTnkStats>();
    }
    public void Destroyed()
    {
        //decrease tank ammo in stats
        stats.stats[caliber]-=maxAmount;
        //decrease equal amounts in all ammo
        if(stats.stats[caliber] < stats.CalculateAmmoVolume(caliber))
        {
            stats.removeExcessAmmo(caliber);
        }
        //TODO explode and create spall
    }
    public void Test()
    {
        Debug.Log(this.GetType().ToString());
    }
    public void Repaired()
    {
        stats.stats[caliber] += maxAmount;
    }
    /*
    public void AddAmmo(string name, string caliber, int amount, float volume)
    {

    }
    public void RemoveAmmo(string name)
    {
        for (int i = storage.Count-1; i>=0; i--)
        {
            if (storage[i].name == name)
            {
                storage[i].amount--;
                if (storage[i].amount <=0)
                {
                    storage.RemoveAt(i);
                }
            }
        }
    }
    public bool ContainsAmmo(string name)
    {
        foreach (var ammo in storage)
        {
            if (ammo.name == name)
            {
                return true;
            }
        }
        return false;
    }
    private void CalculateAmmoVolume()
    {
        curAmount = 0;
        foreach(var ammo in storage)
        {
            curAmount += ammo.amount;
        }
    }
    */
}
