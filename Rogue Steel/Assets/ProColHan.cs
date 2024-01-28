using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ProColHan : MonoBehaviour
{
    public ProStats stats;
    public Rigidbody2D rb;
    public int layer;
    //a
    public Vector2 startPos;
    //b1
    public Vector2 hitPos;
    //b2
    public Vector2 armPos;
    //pen from outside
    public Vector2 OutPen;
    //pen from inside
    public Vector2 InPen;
    //pen from outside
    public float outPenAng;
    //pen from inside
    public float inPenAng;
    //pen offset from perp
    public Vector2 testPen;
    //test angle Dif
    public float testPenAng;
    public float angPen;
    public ModuleInfo othInf;
    public float armDeg;
    public float RicCh;
    public float HitCh;
    public string side;
    public float test;
    public void Awake()
    {
        stats = this.transform.gameObject.GetComponent<ProStats>();
        layer = this.transform.gameObject.layer;
        rb=this.transform.gameObject.GetComponent<Rigidbody2D>();
    }
    public void RayCastHit(GameObject other)
    {
        Debug.Log("Collided");
        if (this.transform.gameObject.layer == other.layer)
        {
            //Debug.Log("Hit Intended");
            if (other.name == "Side Armor(Clone)")
            {
                othInf = other.GetComponent<ModuleInfo>();
                angPen = angle();
                Debug.Log("angle = "+angPen);
                RicCh = angPen / 90;
                HitCh = Random.Range(0f, 100f) / 100;
                if (true/*HitCh+stats.Pen < RicCh+othInf.CurPenRes*/)
                {
                    //Debug.Log("Ricochet with a "+HitCh*100+"% out of "+RicCh*100+"%");
                    ricochet();
                }
                else
                {
                    Debug.Log("Penetrate with a " + HitCh * 100 + "% out of " + RicCh * 100 + "%");
                    //pen();
                }
            }
        }
    }

    //use signed angle later
    public float angle()
    {
        hitPos = new Vector2(this.transform.position.x, this.transform.position.y);
        //Debug.Log("Hit " + other.gameObject.name);
        //Debug.Log(stats.Dam + " " + stats.Pen + " " + stats.startPos + " " + hitPos);
        armDeg = othInf.getDirDeg();
        armPos = othInf.getVector2();
        OutPen.Set(returnx(-1,0,armPos), returny(-1,0, armPos));
        InPen.Set(returnx(1,0, armPos), returny(1,0, armPos));
        outPenAng = Vector2.SignedAngle(stats.startPos - hitPos, OutPen - armPos);
        inPenAng = Vector2.SignedAngle(stats.startPos - hitPos, InPen - armPos);
        
        //shortest angle is closest to perpendicular
        if (Mathf.Abs(outPenAng) < Mathf.Abs(inPenAng))
        {
            Debug.Log("Front");
            //Debug.Log("Start = "+ stats.startPos +" Hit = "+hitPos+" Front Pen = "+OutPen +" armor Pos = "+ armPos);
            //testPen.Set(returnx(-1, 1, armPos), returny(-1, 1, armPos));
            //testPenAng = Vector2.Angle(stats.startPos - hitPos, testPen - armPos);
            //test = testPenAng-outPenAng;
            side = "Front";
            return outPenAng;
        }
        else
        {
            Debug.Log("Back");
            //Debug.Log("Start = " + stats.startPos + " Hit = " + hitPos + " Back Pen = " + InPen + " armor Pos = " + armPos);
            //testPen.Set(returnx(1, 1, armPos), returny(1, 1, armPos));
            //testPenAng = Vector2.Angle(stats.startPos - hitPos, testPen - armPos);
            //test = testPenAng - inPenAng;
            side = "Back";
            return inPenAng;
        }
    }
    public float returny(float d, float a, Vector2 w)
    {
        return (d*Mathf.Sin((armDeg + 90+a) * Mathf.Deg2Rad)) + w.y;
    }
    public float returnx(float d, float a, Vector2 w)
    {
        return (d*Mathf.Cos((armDeg + 90+a) * Mathf.Deg2Rad)) + w.x;
    }

    public void ricochet()
    {
        if (side == "Front")
        {
            Debug.Log("rotation in world"+rb.rotation);
            rb.SetRotation(rb.rotation+180);
            Debug.Log("New Rotation" + rb.rotation+2*angPen);
            //rb.SetRotation(rb.rotation+180+test*2*angPen);
            /*
            //set vel to 0
            rb.velocity = new Vector2(0, 0);
            //re add force
            rb.AddRelativeForce(Vector2.up * stats.Velocity);
            */
        }
    }
    public void penetrate()
    {
        if (side == "Front")
        {
            Debug.Log("rotation in world" + rb.rotation);
            //rb.SetRotation(rb.rotation + test * angPen);
        }
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
