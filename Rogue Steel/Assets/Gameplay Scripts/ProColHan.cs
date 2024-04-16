using UnityEngine;
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
    //hit rolls
    public int PenRoll;
    public int BlockRoll;
    public float Ang;

    public string side;
    public float test;
    private GameObject other;
    public GameObject Projectile;
    private GameObject ProClone;
    //hit stuff
    public bool pen;
    //public GameObject testSquare;
    public void Awake()
    {
        ps = this.transform.gameObject.GetComponent<ProStats>();
        layer = this.transform.gameObject.layer;
        rb=this.transform.gameObject.GetComponent<Rigidbody2D>();
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0,rb.rotation + 90));
    }
    public void RayCastHit(RaycastHit2D ray)
    {
        //Debug.Log("ProColHan RayCastHit Start");
        other = ray.collider.transform.gameObject;
        //Debug.Log("Collided");
        //Debug.Log("Hit Intended");
        if (other.tag == "Armor")
        {
            //Debug.Log(ray.point);
            othInf = other.GetComponent<ModuleInfo>();
            switch (ps.ProType)
            {
                case "Shell":
                    angPen = angle(ray);
                    //Debug.Log("angle = " + angPen);
                    /*
                    RicCh = angPen / 90;
                    HitCh = Random.Range(0f, 100f) / 100;
                    */
                    //----------Hit Calc----------//
                    CalcHit();
                    //----------Hit Calc----------//
                    //Debug.Log("Hit with a " + Ang + "* compared to " + othInf.Ang + "*");
                    if (Ang <= othInf.Ang)
                    {
                        //Debug.Log("Hit with a " + Ang + "* compared to " + othInf.Ang + "*");
                        if (PenRoll > BlockRoll)
                        {
                            //Debug.Log("Penetrate");
                            penetrate(ray);
                        }
                        else
                        {
                            //Debug.Log("No Penetrate");
                            noPenHit();
                        }
                    }
                    else
                    {
                        //Debug.Log("Miss with a " + Ang + "* compared to " + othInf.Ang + "*");
                        ricochet();
                    }
                    break;
                case "Spall":
                    othInf.DamDur(ps.Dam);
                    Destroy(this.transform.gameObject);
                    //function to damage other
                    break;
            }
        }
        else if (other.tag == "Module")
        {
            othInf = other.GetComponent<ModuleInfo>();
            switch (ps.ProType)
            {
                case "Shell":
                    othInf.DamHp(ps.Dam);
                    break;
                case "Spall":
                    othInf.DamHp(ps.Dam);
                    break;
            }
        }
        /*
        else if (other.tag == "Opaque")
        {
            Destroy(other.transform.gameObject);
        }
        //Debug.Log("ProColHan RayCastHit End");
        */
    }

    private void CalcHit()
    {
        //Debug.Log("ProColHan CalcHit Start");
        PenRoll = Random.Range(0, ps.Pen);
        BlockRoll = Random.Range(0, othInf.CurArmor);
        Ang = Mathf.Abs(angPen)-ps.Ang;
        //Debug.Log("ProColHan CalcHit End");
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
        //Debug.Log(ray.point);
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
            //Debug.Log("Back");
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
        othInf.DamDur(ps.Dam-BlockRoll);
        if (side == "Front")
        {
            /*
             * TODO
             * make ricochet random
             */
            //Debug.Log("rotation in world"+rb.rotation);
            rb.SetRotation(rb.rotation + 180 + 2 * angPen);
            //Debug.Log("New Rotation" + rb.rotation+2*angPen);
        }
    }
    public void noPenHit()
    {
        //Debug.Log("ProColHan noPenHit Start");
        //damage durability with damage and penetration
        othInf.DamDur(ps.Dam+PenRoll-BlockRoll);
        Destroy(this.transform.gameObject);
    }
    public void penetrate(RaycastHit2D ray)
    {
        //damage durability
        othInf.DamDur(PenRoll - BlockRoll);
        //decrease Pen
        ps.Pen -= BlockRoll;

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
        //create spall
        for (int i = 0; i < PenRoll - BlockRoll; i++)
        {
            ProClone = Instantiate(Projectile, transform.position, transform.rotation);
            ProClone.transform.Translate(Vector2.up * ray.distance);
            ProClone.transform.gameObject.GetComponent<ProStats>().SetPro("Spall", ps.Dam, 0, 0, ps.Speed, 3);

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
    //----------useful stuff----------//


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
