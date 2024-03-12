using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int health, attack, speed, armor, fireRate, fuel;

    public void EquipItem()
    {
        //Update Stats
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.health += health;
        playerstats.attack += attack;
        playerstats.speed += speed;
        playerstats.armor += armor;
        playerstats.fireRate += fireRate;
        playerstats.fuel += fuel;

        playerstats.UpdateEquipmentStats();

    }

    public void UnEquipItem()
    {
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.health -= health;
        playerstats.attack -= attack;
        playerstats.speed -= speed;
        playerstats.armor -= armor;
        playerstats.fireRate -= fireRate;
        playerstats.fuel -= fuel;

        playerstats.UpdateEquipmentStats();

    }
}
