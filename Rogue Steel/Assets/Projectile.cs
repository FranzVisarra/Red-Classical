using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStats
{
    public float speed;
    public int tfired;
    public float angle;
    public float startpos;
    public float lifetime;

    public ProjectileStats(float Speed, float Angle, float StartPos, float LifeTime)
    {
        speed = Speed;
        angle = Angle;
        startpos = StartPos;
        lifetime = LifeTime;
    }
}
public class Projectile : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
