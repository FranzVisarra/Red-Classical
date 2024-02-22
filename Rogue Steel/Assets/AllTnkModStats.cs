using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TnkModList{
    public string type;
    public Vector2 position;
    public string variant;
    public int MHP;
    public int CHP;
    public int MA;
    public int CA;
    public int MD;
    public int CD;
    TnkModList(string type, Vector2 position, string variant, int MHP, int MA, int MD, int CHP, int CA, int CD)
    {
        this.type = type;
        this.position = position;
        this.variant = variant;
        this.MHP = MHP;
        this.CHP = CHP;
        this.MA = MA;
        this.CA = CA;
        this.MD = MD;
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
