using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;


public class AllBodCom : MonoBehaviour
{
    // Start is called before the first frame update
    private int length = 2;
    private int width = 4;
    //public GameObject cannonTemp;
    //public GameObject cannon;
    //public GameObject armorTemp;
    //private GameObject armor;
    //public GameObject mechanics;
    //private GameObject componentTemp;
    private GameObject component;
    public GameObject TRight;
    public GameObject TLeft;
    //modules
    public GameObject Ammunition;
    public GameObject ArmorBlock;
    public GameObject Engine;
    public GameObject Fuel;
    public GameObject HorizontalDrive;
    public GameObject Radiator;
    public GameObject Crew;
    public GameObject Armor;

    public GameObject Mechanics;
    public ItemTable items;

    public AllTnkStats stats;
    //
    //public Rigidbody2D rb;
    //public string[,] innards;

    public List<TnkModList> components;
    private void Awake()
    {
        Mechanics = GameObject.Find("Mechanics");
        items = Mechanics.GetComponent<ItemTable>();
        stats=transform.GetComponent<AllTnkStats>();
    }
    void Start()
    {
        items = Mechanics.GetComponent<ItemTable>();
        width = 2;
        length = 4;
        this.GetComponent<BoxCollider2D>().size = new Vector2(width, length);
        TRight.GetComponent<BoxCollider2D>().size = new Vector2(1, length);
        TRight.GetComponent<BoxCollider2D>().offset = new Vector2(((float)width / 2) - 0.5f, 0);
        TLeft.GetComponent<BoxCollider2D>().size = new Vector2(1, length);
        TLeft.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f - ((float)width / 2), 0);
        foreach (var comp in components)
        {
            switch (comp.type)
            {
                case "am":
                    InstantiateComponent(comp, Ammunition);
                    foreach (var thing in items.ammo)
                    {
                        if (comp.variant == thing.name)
                        {
                            component.GetComponent<ModuleInfo>().setMaxValues(thing.maxHP, thing.maxArmor, thing.maxDurability);
                            component.GetComponent<AmmoInfo>().setStats(thing.maxAmount,thing.caliber);
                            //check if stats dictionary contains ammo caliber
                            if (stats.stats.ContainsKey(thing.caliber))
                            {
                                stats.stats[thing.caliber] += thing.maxAmount;
                            }
                            else
                            {
                                stats.stats.Add(thing.caliber, thing.maxAmount);//reminder that this dict is max amount of caliber rather than current amount
                            }
                            break;
                        }
                    }
                    break;
                case "en":
                    InstantiateComponent(comp, Engine);
                    foreach (var thing in items.eng)
                    {
                        if (comp.variant == thing.name)
                        {
                            component.GetComponent<ModuleInfo>().setMaxValues(thing.maxHP,thing.maxArmor,thing.maxDurability);
                            component.GetComponent<EngineInfo>().speed = thing.speed;
                            component.GetComponent<EngineInfo>().fuelEfficiency = thing.fuelEfficiency;
                            component.GetComponent<EngineInfo>().AddStats();
                            break;
                        }
                    }
                    break;
                case "fu":
                    InstantiateComponent(comp, Fuel);
                    foreach (var thing in items.fuel)
                    {
                        if (comp.variant == thing.name)
                        {
                            component.GetComponent<ModuleInfo>().setMaxValues(thing.maxHP, thing.maxArmor, thing.maxDurability);
                            component.GetComponent<FuelInfo>().maxFuel = thing.maxFuel;
                            component.GetComponent<FuelInfo>().AddMaxFuelFull();
                            break;
                        }
                    }
                    break;
                case "hd":
                    InstantiateComponent(comp, HorizontalDrive);
                    foreach (var thing in items.hd)
                    {
                        if (comp.variant.Contains(thing.name))
                        {
                            component.GetComponent<ModuleInfo>().setMaxValues(thing.maxHP, thing.maxArmor, thing.maxDurability);
                            component.GetComponent<HDInfo>().rotateSpeed = thing.rotationSpeed;
                            break;
                        }
                    }
                    foreach (var thing in items.can)
                    {
                        component = component.gameObject.transform.GetChild(0).gameObject;
                        if (comp.variant.Contains(thing.name))
                        {
                            component.GetComponent<CannonInfo>().setStats(thing.reloadSpeed, thing.caliber);
                            break;
                        }
                    }
                    //component = instantiate
                    break;
                case "ra"://might remove
                    InstantiateComponent(comp, Radiator);
                    break;
                case "cd":
                    InstantiateComponent(comp, Crew);
                    component.GetComponent<CrewInfo>().CrewType = "Driver";
                    component.GetComponent<CrewInfo>().inModule = 0;
                    break;
                case "cg":
                    InstantiateComponent(comp, Crew);
                    component.GetComponent<CrewInfo>().CrewType = "Gunner";
                    component.GetComponent<CrewInfo>().inModule = 0;
                    break;
                case "cl":
                    InstantiateComponent(comp, Crew);
                    component.GetComponent<CrewInfo>().CrewType = "Loader";
                    component.GetComponent<CrewInfo>().inModule = 0;
                    break;
                case "cr":
                    InstantiateComponent(comp, Crew);
                    component.GetComponent<CrewInfo>().CrewType = "Reserve";
                    component.GetComponent<CrewInfo>().inModule = 5;
                    break;
                case "ar":
                    InstantiateComponent(comp, Armor);
                    foreach (var thing in items.arm)
                    {
                        if (comp.variant == thing.name)
                        {
                            component.GetComponent<ModuleInfo>().setMaxValues(thing.maxHP, thing.maxArmor, thing.maxDurability);
                            //TODO set fuel
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        /*
        //setup
        //--------------------  Tank Components --------------------//
        //mechanics = GameObject.Find("Mechanics");
        //AllTnkPre innardsscript = mechanics.GetComponent<AllTnkPre>();
        //innards = innardsscript.PlTank;
        width = innards.GetLength(0);
        length = innards.GetLength(1);
        //rb = this.GetComponent<Rigidbody2D>();
        this.GetComponent<BoxCollider2D>().size = new Vector2(length, width);
        for (int i = 0; i < width; i++)
        {
            for (int n = 0; n < length; n++)
            {
                //get component
                com(innards[i, n]);
                component = Instantiate(componentTemp, this.transform);
                component.GetComponent<ModuleInfo>().setValues(10, 0, 0);
                //transform component
                component.transform.Translate(0 - ((float)length / 2) + 0.5f + n, ((float)width / 2) - 0.5f - i, -2);
                if (this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    component.layer = LayerMask.NameToLayer("PlayerCollision");
                }
                else if (this.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    component.layer = LayerMask.NameToLayer("EnemyCollision");
                }
            }
        }
        //--------------------  Tank Components --------------------//
        //--------------------      Armor Tile  --------------------//
        //left
        directionalTiles(length, width, 90);
        //right
        directionalTiles(length, width, 270);
        //top
        directionalTiles(width, length, 0);
        //bottom
        directionalTiles(width, length, 180);
        //--------------------      Armor Tile  --------------------//
        //--------------------      Treads      --------------------//
        TRight.GetComponent<BoxCollider2D>().size = new Vector2(1, width);
        TRight.GetComponent<BoxCollider2D>().offset = new Vector2(((float)length / 2) - 0.5f, 0);
        TLeft.GetComponent<BoxCollider2D>().size = new Vector2(1, width);
        TLeft.GetComponent<BoxCollider2D>().offset = new Vector2(0.5f - ((float)length / 2), 0);
        //--------------------      Treads      --------------------//
        */
    }
    /*
    public void setLayer(GameObject temp)
    {
        if (this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            temp.layer = LayerMask.NameToLayer("PlayerCollision");
        }
        else if (this.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            temp.layer = LayerMask.NameToLayer("EnemyCollision");
        }
    }
    */
    public void InstantiateComponent(TnkModList comp, GameObject obj)
    {
        component = Instantiate(obj, this.transform);
        component.GetComponent<ModuleInfo>().setCurrentValues(comp.CHP, comp.CA, comp.CD);
        component.transform.Translate(comp.position);
        component.transform.eulerAngles = new Vector3(0, 0, comp.rotation);
        //setLayer(component);
    }
    /*
    public void com(string inp)
    {
        switch (inp)
        {
            case "am":
                componentTemp = Ammunition;
                break;
            case "en":
                componentTemp = Engine;
                break;
            case "fu":
                componentTemp = Fuel;
                break;
            case "hd":
                componentTemp = HorizontalDrive;
                break;
            case "ra":
                componentTemp = Radiator;
                break;
            case "cd":
                componentTemp = Driver;
                break;
            case "cg":
                componentTemp = Gunner;
                break;
            case "cl":
                componentTemp = Loader;
                break;
            default:
                componentTemp = ArmorBlock;
                break;
        }
    }

    public void directionalTiles(int forward, int side, int degrees)
    {

        for (int i = 0; i < side; i++)
        {
            //Debug.Log(i);
            //armorTemp = GameObject.Find("Armor");
            armor = Instantiate(armorTemp, this.transform);
            if (this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                armor.layer = LayerMask.NameToLayer("PlayerCollision");
            }
            else if (this.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                armor.layer = LayerMask.NameToLayer("EnemyCollision");
            }
            armor.GetComponent<ModuleInfo>().setCurrentValues(0, 5, 5);
            armor.transform.eulerAngles = new Vector3(0, 0, degrees);
            //armor.transform.Translate(0-(float)forward/2,0 - ((float)side / 2) + 0.5f + i, -1);
            armor.transform.Translate(0 - ((float)side / 2) + 0.5f + i, 0 - (float)forward / 2, -2);
        }
    }
    */
}
