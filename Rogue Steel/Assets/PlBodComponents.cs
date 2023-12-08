using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlBodComponents : MonoBehaviour
{
    // Start is called before the first frame update
    private int length = 6;//<->
    private int width = 3;
    //public GameObject cannonTemp;
    //public GameObject cannon;
    public GameObject armorTemp;
    private GameObject armor;
    public GameObject mechanics;
    private GameObject componentTemp;
    private GameObject component;
    private GameObject Modules;
    public string[,] innards;
    void Start()
    {
        //length = 3;
        //setup
        /*--------------------      Cannon      --------------------//
        cannonTemp = GameObject.Find("Cannon");
        cannon = Instantiate(cannonTemp, this.transform);
        cannon.transform.Translate(0, 0, 1);
        cannonTemp.SetActive(false);
        //--------------------      Cannon      --------------------*/
        //--------------------  Tank Components --------------------//
        //mechanics = GameObject.Find("Mechanics");
        PlTankComponent innardsscript = mechanics.GetComponent<PlTankComponent>();
        innards = innardsscript.PlTank;
        width = innards.GetLength(0);
        length = innards.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int n = 0; n < length; n++)
            {
                //get component
                componentTemp = GameObject.Find(com(innards[i, n]));
                component = Instantiate(componentTemp, this.transform);
                //transform component
                component.transform.Translate(0 - ((float)length / 2) + 0.5f + n, ((float)width / 2) - 0.5f - i, -1);
                component.layer = 7;
            }
        }
        //--------------------  Tank Components --------------------//
        //--------------------      Armor Tile  --------------------//
        //front
        directionalTiles(length,width,0);
        //back
        directionalTiles(length,width,180);
        //left
        directionalTiles(width,length,90);
        //right
        directionalTiles(width,length,270);
        armorTemp.SetActive(false);
        //--------------------      Armor Tile  --------------------//
        Modules = GameObject.Find("Modules");
        Modules.SetActive(false);
    }

    public string com(string inp)
    {
        switch (inp)
        {
            case "am":
                return "Ammunition";
            case "ar":
                return "Armor Block";
            case "en":
                return "Engine";
            case "fu":
                return "Fuel";
            case "hd":
                return "Horizontal Drive";
            case "ra":
                return "Radiator";
            case "cd":
                return "Driver";
            case "cg":
                return "Gunner";
            case "cl":
                return "Loader";
            default:
                return "Empty Cell";
        }
    }

    public void directionalTiles(int forward, int side, int degrees)
    {
        
        for (int i = 0; i < side; i++)
        {
            //Debug.Log(i);
            //armorTemp = GameObject.Find("Armor");
            armor = Instantiate(armorTemp, this.transform);
            armor.layer = 7;
            armor.transform.eulerAngles = new Vector3(0,0,degrees);
            armor.transform.Translate(0-(float)forward/2,0 - ((float)side / 2) + 0.5f + i, -1);
        }
    }
}
