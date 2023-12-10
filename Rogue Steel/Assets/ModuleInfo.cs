using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleInfo : MonoBehaviour
{
    public float MaxHp;
    public float CurHp;
    public float MaxPenRes;
    public float CurPenRes;
    public float efficiency;
    public float dirDeg;
    public GameObject testSquare;
    public GameObject testSquareF;
    public void Start()
    {
        testSquare = GameObject.Find("SquareForTest");
    }

    public float getDirDeg()
    {
        return this.transform.eulerAngles.z;
    }

    public Vector2 getVector2()
    {
        return new Vector2(transform.position.x,transform.position.y);
    }
    /*
    public void ArmorHitByPro(float Dam,float Pen, Vector2 sp, Vector2 hp)
    {
        //calculate angle of intercept
        GameObject perprep = Instantiate(testSquare, this.transform);
        perprep.transform.Translate(0, -1, 0);
    }
    */
    /*
    public void tsq(Vector2 i)
    {
        GameObject s = Instantiate(testSquare);
        s.transform.position = new Vector2(i.x,i.y);
    }
    */
    public void setValues(float MHP, float MPR)
    {
        MaxHp = MHP;
        CurHp = MaxHp;
        MaxPenRes = MPR;
        CurPenRes = MaxPenRes;
    }
}
