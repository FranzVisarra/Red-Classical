using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats
{
    public float speed;
    public int tfired;
    public float angle;
    public Vector3 startpos;
    public float lifetime;

    public ProjectileStats(float Speed, float Angle, Vector3 StartPos, float LifeTime)
    {
        speed = Speed;
        angle = Angle;
        startpos = StartPos;
        lifetime = LifeTime;
    }
}
public class Projectile : MonoBehaviour
{
    public List<Projectile> plist = new List<Projectile>();
    public float speed;
    public int tfired;
    public float angle;
    public float startpos;
    public float lifetime;

    public Projectile(float Speed, float Angle, float StartPos, float LifeTime)
    {
        speed = Speed;
        angle = Angle;
        startpos = StartPos;
        lifetime = LifeTime;
    }


    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void fire(float Speed, Quaternion Angle, Vector3 StartPos, float LifeTime)
    {
        //instantiate projectile
        projectile = GameObject.Find("ProjectileThing");
        Instantiate(projectile, StartPos, Angle);
        //plist.Add(new Projectile(Speed, Angle, StartPos, LifeTime));

    }
}
