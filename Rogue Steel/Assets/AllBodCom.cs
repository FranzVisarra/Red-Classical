using UnityEngine;


public class AllBodCom : MonoBehaviour
{
    // Start is called before the first frame update
    private int length = 3;//<->
    private int width = 6;
    //public GameObject cannonTemp;
    //public GameObject cannon;
    public GameObject armorTemp;
    private GameObject armor;
    //public GameObject mechanics;
    private GameObject componentTemp;
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
    public GameObject Driver;
    public GameObject Gunner;
    public GameObject Loader;
    //
    //public Rigidbody2D rb;
    public string[,] innards;
    void Start()
    {
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
    }

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
            armor.GetComponent<ModuleInfo>().setValues(0, 5, 5);
            armor.transform.eulerAngles = new Vector3(0, 0, degrees);
            //armor.transform.Translate(0-(float)forward/2,0 - ((float)side / 2) + 0.5f + i, -1);
            armor.transform.Translate(0 - ((float)side / 2) + 0.5f + i, 0 - (float)forward / 2, -2);
        }
    }
}
