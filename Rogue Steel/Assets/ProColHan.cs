using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.MaterialProperty;
//this class concerns hitting a thing
public class ProColHan : MonoBehaviour
{
    public ProStats ps;
    public Rigidbody2D rb;
    public int layer;
    //a
    public Vector2 startPos;
    //b1
    //ray point
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
    private GameObject other;
    public GameObject Projectile;
    private GameObject ProClone;
    //public GameObject testSquare;
    public void Awake()
    {
        ps = this.transform.gameObject.GetComponent<ProStats>();
        layer = this.transform.gameObject.layer;
        rb=this.transform.gameObject.GetComponent<Rigidbody2D>();
    }
    public void RayCastHit(RaycastHit2D ray)
    {
        other = ray.collider.transform.gameObject;
        Debug.Log("Collided");
        //Debug.Log("Hit Intended");
        if (other.name == "Side Armor(Clone)")
        {
            Debug.Log(ray.point);
            othInf = other.GetComponent<ModuleInfo>();
            switch (ps.ProType) {
                case "AP":
                    angPen = angle(ray);
                    Debug.Log("angle = " + angPen);
                    /*
                    RicCh = angPen / 90;
                    HitCh = Random.Range(0f, 100f) / 100;
                    */
                    if (false/*HitCh+stats.Pen < RicCh+othInf.CurPenRes*/)
                    {
                        //Debug.Log("Ricochet with a "+HitCh*100+"% out of "+RicCh*100+"%");
                        ricochet();
                    }
                    else
                    {
                        //Debug.Log("Penetrate with a " + HitCh * 100 + "% out of " + RicCh * 100 + "%");
                        penetrate(ray);
                    }
                    break;
                case "Spall":
                    Destroy(this.transform.gameObject);
                    //function to damage other
                    break;
            }
        }
    }
    /*
    private GameObject origin;
    private GameObject hit;
    private GameObject paralell;
    private GameObject otherPos;
    */
    //use signed angle later
    public float angle(RaycastHit2D ray)
    {
        Debug.Log(ray.point);
        //Debug.Log("Hit " + other.gameObject.name);
        //Debug.Log(stats.Dam + " " + stats.Pen + " " + stats.startPos + " " + hitPos);
        armDeg = othInf.getDirDeg();
        armPos = othInf.getVector2();
        OutPen.Set(returnx(-1,0,armPos), returny(-1,0, armPos));
        InPen.Set(returnx(1,0, armPos), returny(1,0, armPos));
        /* test square
        origin = Instantiate(testSquare,this.transform);
        origin.transform.position = ray.point;
        hit = Instantiate(testSquare);
        hit.transform.position = OutPen;
        paralell = Instantiate(testSquare);
        paralell.transform.position = InPen;
        otherPos = Instantiate(testSquare);
        otherPos.transform.position = armPos;
        */
        outPenAng = Vector2.SignedAngle((Vector2)this.transform.position - ray.point, OutPen - armPos);
        inPenAng = Vector2.SignedAngle((Vector2)this.transform.position - ray.point, InPen - armPos);
        
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

    public void ricochet()
    {
        if (side == "Front")
        {
            Debug.Log("rotation in world"+rb.rotation);
            rb.SetRotation(rb.rotation+180 +2*angPen);
            Debug.Log("New Rotation" + rb.rotation+2*angPen);
            //rb.transform.Translate(Vector2.up * stats.Speed * Time.deltaTime);
            //rb.SetRotation(rb.rotation+180+test*2*angPen);
            /*
            //set vel to 0
            rb.velocity = new Vector2(0, 0);
            //re add force
            rb.AddRelativeForce(Vector2.up * stats.Velocity);
            */
        }
    }
    public void penetrate(RaycastHit2D ray)
    {
        if (angPen > 0)
        {
            rb.SetRotation(rb.rotation + Random.Range(Random.Range(angPen - 90,0), Random.Range(0,angPen+(float)22.5)));
        }
        else if (angPen < 0)
        {
            rb.SetRotation(rb.rotation + Random.Range(Random.Range(angPen-(float)22.5, 0), Random.Range(0, angPen+90)));
        }
        else
        {
            rb.SetRotation(rb.rotation += Random.Range(-90, 90));
        }
        for (int i = 0; i < 5; i++)
        {
            if (side == "Front")
            {
                //create spall
                ProClone = Instantiate(Projectile, transform.position, transform.rotation);
                ProClone.transform.Translate(Vector2.up * ray.distance);
                ProClone.GetComponent<ProStats>().ProType = "Spall";
                if (angPen > 0)
                {
                    ProClone.transform.gameObject.GetComponent<Rigidbody2D>().SetRotation(rb.rotation + Random.Range(Random.Range(angPen - 90, 0), Random.Range(0, angPen + (float)22.5)));
                }
                else if (angPen < 0)
                {
                    ProClone.transform.gameObject.GetComponent<Rigidbody2D>().SetRotation(rb.rotation + Random.Range(Random.Range(angPen - (float)22.5, 0), Random.Range(0, angPen + 90)));
                }
                else
                {
                    ProClone.transform.gameObject.GetComponent<Rigidbody2D>().SetRotation(rb.rotation += Random.Range((float)-22.5, (float)90));
                }
            }
        }
        
    }
    //----------useful stuff----------
    public float returny(float d, float a, Vector2 w)
    {
        return (d * Mathf.Sin((armDeg + 90 + a) * Mathf.Deg2Rad)) + w.y;
    }
    public float returnx(float d, float a, Vector2 w)
    {
        return (d * Mathf.Cos((armDeg + 90 + a) * Mathf.Deg2Rad)) + w.x;
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
