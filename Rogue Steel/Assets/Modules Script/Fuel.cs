using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField]
    RectTransform FuelFill;

    public float maxFuel;
    public float FuelAmount;
    public float FuelBurnRate = 0.3f;

    public float refuelAmount = 50f;


    // Start is called before the first frame update
    private void Start()
    {
        FuelAmount = maxFuel;
    }


    // Update is called once per frame
    void Update()
    {
        BurnFuel();
        SetFuelAmount();

        /* // In case we need to do the refuel in the game.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Refuel();
        }
        */
    }

    void SetFuelAmount() // Update the amount of Fuel bar
    {
        float Fuelscale = FuelAmount / maxFuel;
        FuelFill.localScale = new Vector3(Fuelscale, 1f, 1f);
        
    }

    void BurnFuel() 
    {
        FuelAmount -= FuelBurnRate * Time.deltaTime;
        FuelAmount = Mathf.Max(FuelAmount, 0f);
    }

    void Refuel() 
    {
        FuelAmount += refuelAmount;
        FuelAmount = Mathf.Min(FuelAmount, maxFuel);
    }
}
