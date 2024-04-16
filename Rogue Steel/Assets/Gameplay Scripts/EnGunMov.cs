using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnGunMov : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 targPos;
    public Vector2 curPos;
    public Quaternion targRot;
    public float angle;
    public GameObject Enemy;
    public EnTnkStats EScript;
    public CannonInfo info;
    public float time;
    public GameObject Chassis;//this chassis
    public GameObject HDrive;
    public HDInfo hdInfo;

    public List<RaycastHit2D> MuzzleRay = new List<RaycastHit2D>();
    public List<RaycastHit2D> lRay = new List<RaycastHit2D>();
    public List<RaycastHit2D> rRay = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;

    public GameObject targ;

    public AllTnkStats stats;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Enemy Gun Script Start");
        moveSpeed = 0.5f;
        targPos = new Vector2(0,0);
        Enemy = this.transform.parent.parent.parent.gameObject;
        EScript = Enemy.GetComponent<EnTnkStats>();
        Chassis = this.transform.parent.parent.gameObject;
        HDrive = this.transform.parent.gameObject;
        hdInfo = HDrive.GetComponent<HDInfo>();
        stats = transform.parent.parent.GetComponent<AllTnkStats>();
        time = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " LateUpdate Start");
        //Debug.Log("Enemy Gun Script Update");
        if (stats.gunnerStatus)
        {
            angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
            targRot = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, hdInfo.curRotSp * Time.deltaTime);
        }
        string AIState = EScript.AiState;
        switch (AIState)
        {
            case "Patrol":
                //targPos = patrol position;
                break;
            case "Investigate":
            case "Attack":
                targPos = EScript.dlist[0].pos;
                break;
        }
        //Debug.Log(this.GetType().ToString() + " LateUpdate End");
    }
    private void FixedUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " FixedUpdate Start");
        if (time >= 50)
        {
            time = 0;
        }
        else
        {
            time++;
        }
        Sight();
        if (EScript.DetState=="Sound Heard")
        {
            targPos = EScript.Direction;
        }
        //Debug.Log(this.GetType().ToString() + " FixedUpdate End");
    }
    private void Sight()
    {
        SightRay();
        Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), cf2d, MuzzleRay, info.detectionLength);
        if (RayHit(MuzzleRay))
        {
            EScript.enemySeen(targ.transform.position);
            if (EScript.AiState=="Attack" && info.shootStatus == "Ready")
            { info.shootStatus = "Shoot"; }
        }
    }

    bool RayHit(List<RaycastHit2D> Ray)
    {
        float totDist = info.detectionLength;
        foreach (RaycastHit2D ray in Ray)
        {
            
            if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Player") && ray.distance <= totDist)
            {
                targ = ray.collider.gameObject;
                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * ray.distance, Color.white);
                return true;
            }
            else if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Obstruction"))
            {
                if (ray.collider.gameObject.tag == "Opaque")
                {
                    Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * ray.distance, Color.white);
                    return false;
                }
                else if (ray.collider.gameObject.tag == "Translucent")
                {
                    totDist = (totDist - ray.distance) / 4 + ray.distance;
                    Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * totDist, Color.white);
                }
            }
        }
        return false;
    }
    void SightRay()
    {
        //----------ray bound----------//
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, -25) * this.gameObject.transform.right * -10, Color.magenta);
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, 25) * this.gameObject.transform.right * -10, Color.magenta);
        //----------ray bound----------//
        Physics2D.Raycast(this.transform.position, Quaternion.Euler(0, 0, 25 - time % 25) * this.gameObject.transform.right * -10, cf2d, lRay, info.detectionLength);
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, 25 - time % 25) * this.gameObject.transform.right * -10, Color.red);
        
        Physics2D.Raycast(this.transform.position, Quaternion.Euler(0, 0, time % 25 - 25) * this.gameObject.transform.right * -100, cf2d, rRay, info.detectionLength);
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, time % 25 - 25) * this.gameObject.transform.right * -10, Color.blue);
        if (RayHit(lRay)||RayHit(rRay))
        {
            EScript.enemySeen(targ.transform.position);
            targPos = targ.transform.position;
        }
    }
}
