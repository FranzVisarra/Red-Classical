using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int health, attack, speed, armor, fireRate, fuel;
    [SerializeField]
    private TMP_Text healthText, attackText, speedText, armorText, fireRateText, fuelText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
        speedText.text = speed.ToString();
        armorText.text = armor.ToString();
        fireRateText.text = fireRate.ToString();
        fuelText.text = fuel.ToString();
    }
}
