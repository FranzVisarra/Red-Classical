using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ProColHan : MonoBehaviour
{
    public ProStats stats;
    public int layer;
    //a
    public Vector2 startPos;
    //b1
    public Vector2 hitPos;
    //b2
    public Vector2 armPos;
    //c
    public Vector2 FrontPen;
    //c
    public Vector2 BackPen;
    //pen from outside
    public float outPenAng;
    //pen from inside
    public float inPenAng;
    public float angPen;
    public ModuleInfo othInf;
    public float armDeg;
    public void Awake()
    {
        stats = this.transform.gameObject.GetComponent<ProStats>();
        layer = this.transform.gameObject.layer;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided");
        if (this.transform.gameObject.layer == other.transform.gameObject.layer)
        {
            //Debug.Log("Hit Intended");
            if (other.gameObject.name == "Side Armor(Clone)")
            {
                angPen = angle(other);
                Debug.Log("angle = "+angPen);
            }
        }
    }

    public float angle(Collider2D other)
    {
        hitPos = new Vector2(this.transform.position.x, this.transform.position.y);
        //Debug.Log("Hit " + other.gameObject.name);
        //Debug.Log(stats.Dam + " " + stats.Pen + " " + stats.startPos + " " + hitPos);
        othInf = other.gameObject.GetComponent<ModuleInfo>();
        armDeg = othInf.getDirDeg();
        armPos = othInf.getVector2();
        FrontPen.Set(returnx(-1,0), returny(-1,0));
        //othInf.tsq(FrontPen);
        BackPen.Set(returnx(1,0), returny(1,0));
        //othInf.tsq(BackPen);
        outPenAng = Vector2.Angle(stats.startPos - hitPos, FrontPen - armPos);
        inPenAng = Vector2.Angle(stats.startPos - hitPos, BackPen - armPos);
        //shortest angle is closest to perpendicular
        if (outPenAng < inPenAng)
        {
            Debug.Log("Start = "+ stats.startPos +" Hit = "+hitPos+" Front Pen = "+FrontPen +" armor Pos = "+ armPos);
            Debug.Log("Front");
            return outPenAng;
        }
        else
        {
            Debug.Log("Back");
            Debug.Log("Start = " + stats.startPos + " Hit = " + hitPos + " Back Pen = " + BackPen +" armor Pos = "+ armPos);
            return inPenAng;
        }
    }
    public float returny(float d, float a)
    {
        return (d*Mathf.Sin((armDeg + 90+a) * Mathf.Deg2Rad)) + armPos.y;
    }
    public float returnx(float d, float a)
    {
        return (d*Mathf.Cos((armDeg + 90+a) * Mathf.Deg2Rad)) + armPos.x;
    }
    //public void ArmorHitByPro(float Dam, float Pen, Vector2 sp, Vector2 hp)
    //{
    //calculate angle of intercept
    /*
    perprep.transform.Translate(0, -1, 0);
    Dam = Dam * CurPenRes / 100;
    CurHp -= Dam;
    efficiency = CurHp / MaxHp;
    */
    //}

}
