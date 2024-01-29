using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProStats : MonoBehaviour
{
    public float Dam;
    public float Speed;
    public float Pen;
    //type of round
    public string ProType;
    //target of projectile
    public string ProHit;
    public Vector2 startPos;
    public ProMov pm;
    // Start is called before the first frame update
    void Start()
    {
        Dam = 5f;
        Speed = 50f;
        pm.Speed = Speed;
        Pen = 0.5f;
        switch (ProType){
            case "AP":
                ProHit = "Armor";
                break;
            case "Spall":
                ProHit = "Module Armor";
                break;
        }
        Debug.Log(ProType + " Round");
        //startPos = new Vector2(transform.position.x,transform.position.y);
    }
    /*
    public void setStartPos(Vector2 i)
    {
        startPos = i;
    }
    */
}
