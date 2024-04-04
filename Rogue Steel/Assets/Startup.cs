using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives
{
    public string text;
    public string shortText;
    public int amount;
    public int progress;
    public int reward;
    public string keyword;

    public Objectives(string text, string shortText, int amount, int progress, int reward, string keyword)
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
    public List<TnkModList> tnk;
    public List<StoredAmmo> storage;
    public List<Objectives> obj;

    // Start is called before the first frame update
    void Awake()
    {
        tnk = new List<TnkModList>();
        storage = new List<StoredAmmo>();
        obj = new List<Objectives>();
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

        //load objectives
        obj.Add(new Objectives("Kill light tanks", "Light tanks:",0,5,500,"kill light"));
        obj.Add(new Objectives("Capture crates", "Crates:",0,2,1000, "cap crate"));

        //load map
        MapGen();

    }
    public void MapGen()
    {
        //TODO do map generation
    }
}
