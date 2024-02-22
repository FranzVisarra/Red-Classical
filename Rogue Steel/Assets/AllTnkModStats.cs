using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TnkModList{
    public string type;
    public Vector2 position;
    public int rotation;
    public string variant;
    public int CHP;
    public int CA;
    public int CD;
    public TnkModList(string type, Vector2 position, int rotation, string variant, int CHP, int CA, int CD)
    {
        this.type = type;
        this.position = position;
        this.rotation = rotation;
        this.variant = variant;
        this.CHP = CHP;
        this.CA = CA;
        this.CD = CD;
    }
}
public class ModHealth
{

}
public class ModStats
{

}
public class AllTnkModStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
