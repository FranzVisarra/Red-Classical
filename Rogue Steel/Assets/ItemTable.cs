using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fuel
{
    public string name;
    public int maxHP;
    public int maxArmor;
    public int maxDurability;
    public float maxFuel;
    public Fuel(string name, float maxFuel, int maxHP, int maxArmor, int maxDurability)
    {
        this.name = name;
        this.maxFuel = maxFuel;
        this.maxHP = maxHP;
        this.maxArmor = maxArmor;
        this.maxDurability = maxDurability;
    }
}
public class HorizontalDrive
{
    public string name;
    public int maxHP;
    public int maxArmor;
    public int maxDurability;
    public float rotationSpeed;

    public HorizontalDrive(string name, float rotationSpeed, int maxHP, int maxArmor, int maxDurability)
    {
        this.name = name;
        this.rotationSpeed = rotationSpeed;
        this.maxHP = maxHP;
        this.maxArmor = maxArmor;
        this.maxDurability = maxDurability;
    }
}
public class Engine
{
    public string name;
    public int maxHP;
    public int maxArmor;
    public int maxDurability;
    public float fuelEfficiency;
    public Engine(string name, float fuelEfficiency, int maxHP, int maxArmor, int maxDurability)
    {
        this.name = name;
        this.fuelEfficiency = fuelEfficiency;
        this.maxHP = maxHP;
        this.maxArmor = maxArmor;
        this.maxDurability = maxDurability;
    }
}
public class Cannon
{
    public string name;
    public string caliber;
    public float reloadSpeed;
    public Cannon(string name, string caliber, float reloadSpeed)
    {
        this.name = name;
        this.caliber = caliber;
        this.reloadSpeed = reloadSpeed;
    }
}
public class Projectile
{
    public string name;
    //damage to stuff
    public int Dam;
    public int Pen;
    public float Ang;
    //stats
    public string Caliber;
    public float Speed;
    public float LifeTime;
    public string type;
    public Projectile(string name,string caliber, string type, int dam, int pen, float ang, float speed, float lifeTime)
    {
        this.name = name;
        this.type = type;
        Dam = dam;
        Pen = pen;
        Ang = ang;
        Caliber = caliber;
        Speed = speed;
        LifeTime = lifeTime;
    }
}
public class ItemTable : MonoBehaviour
{
    public List<Fuel> fuel;
    public List<HorizontalDrive> hd;
    public List<Engine> eng;
    public List<Cannon> can;
    public List<Projectile> pro;
    // Start is called before the first frame update
    void Awake()
    {
        fuel = new List<Fuel>();
        hd = new List<HorizontalDrive>();
        eng = new List<Engine>();
        can = new List<Cannon>();
        pro = new List<Projectile>();
        fuel.Add(new Fuel("Small", 50, 10, 1, 10));
        hd.Add(new HorizontalDrive("Basic", 1, 10, 1, 10));
        eng.Add(new Engine("Basic", 50, 10, 1, 10));
        can.Add(new Cannon("Svarsky 30mm", "30mm", 3));
        pro.Add(new Projectile("30mm AP", "30mm", "Shell", 10, 10, 0, 50, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
