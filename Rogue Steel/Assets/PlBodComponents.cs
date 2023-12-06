using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlBodComponents : MonoBehaviour
{
    // Start is called before the first frame update
    private int width = 3;
    private int length = 6;//<->
    private GameObject Modules;
    private GameObject cannonTemp;
    private GameObject cannon;
    private GameObject armorTemp;
    private GameObject armor;
    private GameObject mechanics;
    private GameObject componentTemp;
    private GameObject component;

    public string[,] innards;
    void Start()
    {
        //setup
        /*----------Cannon----------
        cannonTemp = GameObject.Find("Cannon");
        cannon = Instantiate(cannonTemp, this.transform);
        cannon.transform.Translate(0, 0, -2);
        cannonTemp.SetActive(false);
        //----------Cannon----------*/

        //----------Tank Components----------
        mechanics = GameObject.Find("Mechanics");
        PlTankComponents innardsscript = mechanics.GetComponent<PlTankComponents>();
        innards = innardsscript.PlTank;
        width = innards.GetLength(0);
        length = innards.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int n = 0; n < length; n++)
            {
                //get component
                componentTemp = GameObject.Find(com(innards[i,n]));
                component = Instantiate(componentTemp, this.transform);
                //transform component
                component.transform.Translate(0 - ((float)length / 2) + 0.5f + n, ((float)width / 2) - 0.5f - i,-1);
            }
        }
        //----------Tank Components----------
        //----------Armor Tile----------
        //front
        directionalTiles(length, width, 0);
        //back
        directionalTiles(length, width, 180);
        //left
        directionalTiles(width, length, 90);
        //right
        directionalTiles(width, length, 270);
        armorTemp.SetActive(false);
        //----------Armor Tile----------
        Modules = GameObject.Find("Modules");
        Modules.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void directionalTiles(int forward, int side, int degrees)
    {
        
        for (int i = 0; i < side; i++)
        {
            //Debug.Log(i);
            armorTemp = GameObject.Find("Side Armor");
            armor = Instantiate(armorTemp, this.transform);
            armor.transform.eulerAngles = new Vector3(0,0,degrees);
            armor.transform.Translate(0-(float)forward/2,0 - ((float)side / 2) + 0.5f + i, -1);
        }
    }
    
    public string com(string inp)
    {
        switch (inp)
        {
            case "am":
                return "Ammunition";
                break;
            case "ar":
                return "Armor";
                break;
            case "en":
                return "Engine";
                break;
            case "fu":
                return "Fuel";
                break;
            case "hd":
                return "Horizontal Drive";
                break;
            case "ra":
                return "Radiator";
                break;
            case "cd":
                return "Driver";
                break;
            case "cg":
                return "Gunner";
                break;
            case "cl":
                return "Loader";
                break;
            default:
                return "Empty Cell";
        }
    }
}
