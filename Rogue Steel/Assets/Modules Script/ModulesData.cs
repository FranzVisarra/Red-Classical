using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesData : MonoBehaviour
{
    public void UpdateINfo(GameObject Module, GameObject FuelTank)
    {
        switch (Module.name)
        {
            case "Ammunition":
            case "Engine":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            case "Fuel":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = 0.05f;
                        FuelTank.GetComponent<Fuel>().maxFuel = 150f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = 0.08f;
                        FuelTank.GetComponent<Fuel>().maxFuel = 200f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = 0.03f;
                        FuelTank.GetComponent<Fuel>().maxFuel = 100f;
                        break;
                }
                break;
            case "HorizontalDrive":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            case "Radiator":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            case "Driver":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            case "Gunner":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            case "Loader":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            case "ArmorBlock":
                switch (Module.GetComponent<ModuleInfo>().Model)
                {
                    case 2:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //28
                        Module.GetComponent<ModuleInfo>().efficiency = -0.1f;
                        break;
                    case 3:
                        Module.GetComponent<ModuleInfo>().MaxHp += Module.GetComponent<ModuleInfo>().Model * 14; //42
                        Module.GetComponent<ModuleInfo>().efficiency = -0.15f;
                        break;
                    default:
                        Module.GetComponent<ModuleInfo>().MaxHp = 15;
                        Module.GetComponent<ModuleInfo>().efficiency = -0.05f;
                        break;
                }
                break;
            default:
                Module.GetComponent<ModuleInfo>().MaxArmor = 3;
                Module.GetComponent<ModuleInfo>().MaxDur += Module.GetComponent<ModuleInfo>().Model * 6; //12
                break;
        }



    }
}
