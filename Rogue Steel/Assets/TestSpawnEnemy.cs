using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject InstEnemy;
    public List<TnkModList> tnk;
    public List<StoredAmmo> storage;
    // Start is called before the first frame update
    void Start()
    {
        tnk = new List<TnkModList>();
        storage = new List<StoredAmmo>();
        //inner components
        tnk.Add(new TnkModList("cd", new Vector2((float)0.5, (float)1.5), 0, "", 10, 1, 10));
        tnk.Add(new TnkModList("en", new Vector2(-(float)0.5, (float)1.5), 0, "Basic", 10, 1, 10));
        //tnk.Add(new TnkModList("hd", new Vector2((float)0.5, (float)0.5), 90, "Basic Svarsky 30mm", 10, 1, 10));
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
        InstEnemy = Instantiate(Enemy, new Vector3(10,10,0),new Quaternion());
        InstEnemy.GetComponentInChildren<AllBodCom>().components = tnk;
        InstEnemy.GetComponentInChildren<AllTnkStats>().storage = storage;
        //InstEnemy.GetComponentInChildren<AllBodCom>().innards = Rcvd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
