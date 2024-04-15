using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectives
{
    public string text;
    public string shortText;
    public int amount;
    public int progress;
    public int reward;
    public string keyword;

    public Objectives(string text, string shortText, int progress, int amount, int reward, string keyword)
    {
        this.text = text;
        this.shortText = shortText;
        this.amount = amount;
        this.progress = progress;
        this.reward = reward;
        this.keyword = keyword;
    }
    public bool CheckCompletion()
    {
        if (progress >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public class Startup : MonoBehaviour
{
    public GameObject Player;
    private GameObject InstPlay;
    public GameObject Map;
    public List<TnkModList> tnk;
    public List<StoredAmmo> storage;
    public List<Objectives> obj;
    public int mapSize;
    public GameObject tree;
    public UIHandling UIH;
    public List<inventory> inv;
    public GameObject encounter;
    public Dictionary<int,string> encounterName = new Dictionary<int, string>();
    public Dictionary<int, int> encounterAmount = new Dictionary<int, int>();

    public bool extract = false;

    // Start is called before the first frame update
    void Awake()
    {
        mapSize = 20;//20 x 20
        tnk = new List<TnkModList>();
        storage = new List<StoredAmmo>();
        obj = new List<Objectives>();
        inv = new List<inventory>();
        //inner components
        tnk.Add(new TnkModList("cd", new Vector2((float)0.5, (float)1.5), 0, "", 10, 1, 10));
        tnk.Add(new TnkModList("en", new Vector2(-(float)0.5, (float)1.5), 0, "Basic", 10, 1, 10));
        tnk.Add(new TnkModList("hd", new Vector2((float)0.5, (float)0.5), 90, "Basic Svarsky 30mm", 10, 1, 10));
        tnk.Add(new TnkModList("cg", new Vector2(-(float)0.5, (float)0.5), 0, "", 10, 1, 10));
        tnk.Add(new TnkModList("cl", new Vector2((float)0.5, -(float)0.5), 0, "", 10, 1, 10));
        tnk.Add(new TnkModList("am", new Vector2(-(float)0.5, -(float)0.5), 0, "Basic 30mm", 10, 1, 10));
        tnk.Add(new TnkModList("fu", new Vector2((float)0.5, -(float)1.5), 0, "Small", 10, 1, 10));
        tnk.Add(new TnkModList("fu", new Vector2(-(float)0.5, -(float)1.5), 0, "Small", 10, 1, 10));
        //armor
        //front
        tnk.Add(new TnkModList("ar", new Vector2(-(float)0.5, (float)2.1), 0, "Basic", 0, 5, 10));
        tnk.Add(new TnkModList("ar", new Vector2((float)0.5, (float)2.1), 0, "Basic", 0, 5, 10));
        //back
        tnk.Add(new TnkModList("ar", new Vector2(-(float)0.5, -(float)2.1), 180, "Basic", 0, 5, 10));
        tnk.Add(new TnkModList("ar", new Vector2((float)0.5, -(float)2.1), 180, "Basic", 0, 5, 10));
        //side
        //ammo
        storage.Add(new StoredAmmo("30mm AP", "30mm", 50));
        storage.Add(new StoredAmmo("30mm APC", "30mm", 30));
        //load player
        InstPlay = Instantiate(Player);
        InstPlay.GetComponentInChildren<AllBodCom>().components = tnk;

        InstPlay.GetComponentInChildren<AllTnkStats>().storage = storage;
        InstPlay.GetComponentInChildren<AllTnkStats>().tnkName = "Player";
        //InstPlay.transform.Find("Chassis").Find("Tank Image").GetComponent<RectTransform>().

        //load objectives
        obj.Add(new Objectives("Kill light tanks", "Light Tanks:",0,5,500,"kill light"));
        obj.Add(new Objectives("Capture crates", "Crates:",0,2,1000, "cap crate"));
        

        //load map
        MapGen();

    }
    public void MapGen()
    {
        string[] enCounterTiles = new string[9];
        int tileCount = 8;
        enCounterTiles[4] = "Spawn";
        encounterName.Add(4,"Spawn");
        encounterAmount.Add(4, 1);
        //Debug.Log("Length: "+enCounterTiles.Length);
        foreach (var obj in obj)
        {
            int randomTile = Random.Range(0, tileCount);
            int iterated = 0;
            for (int i = 0; i < enCounterTiles.Length; i++)
            {
                //Debug.Log("i: " + i);
                if (enCounterTiles[i] == null)
                {
                    if (iterated == randomTile)
                    {
                        enCounterTiles[i] = obj.shortText;
                        encounterName.Add(i, obj.shortText);
                        encounterAmount.Add(i, obj.amount);
                        tileCount--;
                        break;
                    }
                    iterated++;
                }
            }
                
        }
        //TODO do map generation
        string[,] nbn = new string[,] { { "forest", "forest", "forest" }, { "forest", "", "forest" }, { "forest", "forest", "forest" } };
        tileCount = 0;//3x3 tiles

        for (int yp = 0; yp < nbn.GetLength(0); yp++)
        {
            for (int xp = 0; xp < nbn.GetLength(1); xp++)
            {
                Dictionary<Vector2, string> tile = ReturnTile(nbn[yp,xp]);
                foreach (var item in tile)
                {
                    float x = (item.Key.x - (mapSize / 2) + 0.5f) + (mapSize * (xp - (nbn.GetLength(0) / 2)));
                    float y = (item.Key.y - (mapSize / 2) + 0.5f) + (mapSize * (yp - (nbn.GetLength(1) / 2)));
                    switch (item.Value)
                    {
                        case "tree":
                            Instantiate(tree, new Vector3(x, y, -1), new Quaternion());
                            break;
                    }
                }
                //get chance for thing to spawn
                float xpos = mapSize * (xp - (nbn.GetLength(0) / 2));
                float ypos = mapSize * (yp - (nbn.GetLength(1) / 2));
                if (encounterName.ContainsKey(tileCount))
                {
                    GameObject tempObj = Instantiate(encounter);
                    tempObj.transform.Translate(new Vector3(xpos, ypos, 0));
                    Debug.Log("encounter spawned");
                    tempObj.transform.GetComponent<TestSpawnEnemy>().setEncounter(encounterName[tileCount], encounterAmount[tileCount]);
                }
                tileCount++;
            }
        }
    }
    public Dictionary<Vector2,string> ReturnTile(string type)
    {
        Dictionary<Vector2, string> tile = new Dictionary<Vector2, string>();
        int checkAdjacent(string orient,Vector2 pos, string item)
        {
            int surround = 0;
            /* 1X
             * 234
             */
            if (orient.Contains("1") && tile[new Vector2(pos.x - 1, pos.y)].Contains(item))
            {
                surround++;
            }
            if (orient.Contains("2") && tile[new Vector2(pos.x - 1, pos.y - 1)].Contains(item))
            {
                surround++;
            }
            if (orient.Contains("3") && tile[new Vector2(pos.x, pos.y - 1)].Contains(item))
            {
                surround++;
            }
            if (orient.Contains("4") && tile[new Vector2(pos.x + 1, pos.y - 1)].Contains(item))
            {
                surround++;
            }
            return surround;
        }
        string tileChance(int surround, string item)
        {
            if (Random.Range(0, surround + 1) == 0)
            {
                return item;
            }
            else
            {
                return "none";
            }
        }
        switch (type)
        {
            case "forest":
                for(int y = 0; y < mapSize; y++)
                {
                    for (int x = 0; x < mapSize; x++)
                    {
                        if (type == "forest")
                        {
                            //detect adjacent during creation
                            int surround = 0;
                            string item = "";
                            if (x == 0)
                            {
                                /* XOOOO
                                 * XOOOO
                                 * XOOOO
                                 * XOOOO
                                 * XOOOO
                                 */
                                if (y == 0)
                                {
                                    /* OOOOO
                                     * OOOOO
                                     * OOOOO
                                     * OOOOO
                                     * XOOOO
                                     */
                                    item = tileChance(0, "tree");
                                    //list
                                    tile.Add(new Vector2(x, y), item);
                                }
                                else//everything between mapsize-1 and 0 inclusive
                                {
                                    /* XOOOO
                                     * X4OOO
                                     * X4OOO
                                     * X4OOO
                                     * 34OOO
                                     */
                                    surround = checkAdjacent("34", new Vector2(x, y), "tree");
                                    item = tileChance(surround, "tree");
                                    tile.Add(new Vector2(x, y), item);
                                }
                            }
                            else if (x == mapSize-1)
                            {
                                /* OOOOX
                                 * OOOOX
                                 * OOOOX
                                 * OOOOX
                                 * OOOOX
                                 */
                                if (y == 0)
                                {
                                    /* OOOOO
                                     * OOOOO
                                     * OOOOO
                                     * OOOOO
                                     * OOO1X
                                     */
                                    surround = checkAdjacent("1", new Vector2(x, y), "tree");
                                    item = tileChance(surround, "tree");
                                    //list
                                    tile.Add(new Vector2(x, y), item);
                                }
                                else//everything between mapsize-1 and 0 inclusive
                                {
                                    /* OOO1X
                                     * OOO2X
                                     * OOO2X
                                     * OOO2X
                                     * OOO23
                                     */
                                    surround = checkAdjacent("123", new Vector2(x, y), "tree");
                                    item = tileChance(surround, "tree");
                                    tile.Add(new Vector2(x, y), item);
                                }
                            }
                            else
                            {
                                /* OXXXO
                                 * OXXXO
                                 * OXXXO
                                 * OXXXO
                                 * OXXXO
                                 */
                                if (y == 0)
                                {
                                    /* OOOOO
                                     * OOOOO
                                     * OOOOO
                                     * OOOOO
                                     * 1XXXO
                                     */
                                    surround = checkAdjacent("1", new Vector2(x, y), "tree");
                                    item = tileChance(surround, "tree");
                                    //list
                                    tile.Add(new Vector2(x, y), item);
                                }
                                else//everything between mapsize-1 and 0 inclusive
                                {
                                    /* 1XXXO
                                     * 2XXXO
                                     * 2XXXO
                                     * 2XXXO
                                     * 22234
                                     */

                                    surround = checkAdjacent("1234", new Vector2(x, y), "tree");
                                    item = tileChance(surround, "tree");
                                    tile.Add(new Vector2(x, y), item);
                                }
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
        return tile;
    }
    public void Lose()
    {
        GameData.score = UIH.credits;
        GameData.tnk = tnk;
        //inventory?
        FileInteraction.SetFile();
        SceneManager.LoadScene("Inventory Scene");
    }
    public void Win()
    {
        GameData.score = UIH.credits;
        GameData.tnk = tnk;
        //inventory?
        FileInteraction.SetFile();
        SceneManager.LoadScene("Inventory Scene");
    }
    public void ToggleWin()
    {
        extract = true;
    }
}
