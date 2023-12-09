using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlBodComponents : MonoBehaviour
{
    // Start is called before the first frame update
    private int length = 3;//<->
    private int width = 6;
    //public GameObject cannonTemp;
    //public GameObject cannon;
    public GameObject armorTemp;
    private GameObject armor;
    public GameObject mechanics;
    private GameObject componentTemp;
    private GameObject component;
    private GameObject Modules;
    public GameObject TRight;
    public GameObject TLeft;
    public Rigidbody2D rb;
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
        rb = this.GetComponent<Rigidbody2D>();
        this.GetComponent<BoxCollider2D>().size = new Vector2(length,width);
        for (int i = 0; i < width; i++)
        {
            for (int n = 0; n < length; n++)
            {
                //get component
                componentTemp = GameObject.Find(com(innards[i, n]));
                component = Instantiate(componentTemp, this.transform);
                component.GetComponent<ModuleInfo>().setValues(10, 10);
                //transform component
                component.transform.Translate(0 - ((float)length / 2) + 0.5f + n, ((float)width / 2) - 0.5f - i, -1);
                component.layer = 7;
            }
        }
        //--------------------  Tank Components --------------------//
        //--------------------      Armor Tile  --------------------//
        //left
        directionalTiles(length,width,90);
        //right
        directionalTiles(length,width,270);
        //top
        directionalTiles(width,length,0);
        //bottom
        directionalTiles(width,length,180);
        armorTemp.SetActive(false);
        //--------------------      Armor Tile  --------------------//
        //--------------------      Treads      --------------------//
        TRight.GetComponent<BoxCollider2D>().size = new Vector2(1,width);
        TRight.GetComponent<BoxCollider2D>().offset = new Vector2(((float)length/2) - 0.5f, 0);
        TLeft.GetComponent<BoxCollider2D>().size = new Vector2(1,width);
        TLeft.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f-((float)length / 2), 0);
        //--------------------      Treads      --------------------//
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
            armor.GetComponent<ModuleInfo>().setValues(50,50);
            armor.transform.eulerAngles = new Vector3(0,0,degrees);
            //armor.transform.Translate(0-(float)forward/2,0 - ((float)side / 2) + 0.5f + i, -1);
            armor.transform.Translate(0 - ((float)side / 2) + 0.5f + i, 0 - (float)forward / 2,-1);
        }
    }
}
