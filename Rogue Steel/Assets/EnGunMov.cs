using System.Collections.Generic;
using UnityEngine;

public class EnGunMov : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Vector2 targPos;
    public Vector2 curPos;
    public Quaternion targRot;
    public float angle;
    public GameObject Enemy;
    public EnTnkStats EScript;
    public CannonInfo info;
    public float time;

    public List<RaycastHit2D> MuzzleRay = new List<RaycastHit2D>();
    public List<RaycastHit2D> lRay = new List<RaycastHit2D>();
    public List<RaycastHit2D> rRay = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;

    public GameObject targ;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        targPos = new Vector2(0,0);
        Enemy = this.transform.parent.parent.parent.gameObject;
        EScript = Enemy.GetComponent<EnTnkStats>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
        targRot = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if(time >= 50)
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
            targPos = EScript.TestNoiseDirection;
        }
    }
    private void Sight()
    {
        SightRay();
        Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), cf2d, MuzzleRay, info.detectionLength);
        if (RayHit(MuzzleRay))
        {
            if (info.shootStatus == "Ready")
            { info.shootStatus = "Shoot"; }
        }
        /*
        float totDist = info.detectionLength;
        foreach (RaycastHit2D ray in MuzzleRay)
        {
            if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Obstruction"))
            {
                if (ray.collider.gameObject.tag=="Opaque")
                {
                    Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left)*ray.distance,Color.white);
                    break;
                }
                else if (ray.collider.gameObject.tag == "Translucent")
                {
                    totDist=(totDist-ray.distance)/4+ray.distance;
                    Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * totDist, Color.white);
                }
            }
            else if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Player")&&ray.distance<=totDist)
            {
                if (info.shootStatus == "Ready")
                { info.shootStatus = "Shoot"; }
                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * ray.distance, Color.white);
                break;
            }
        }
        */
    }

    bool RayHit(List<RaycastHit2D> Ray)
    {
        float totDist = info.detectionLength;
        foreach (RaycastHit2D ray in Ray)
        {
            if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Obstruction"))
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
            else if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Player") && ray.distance <= totDist)
            {
                targ = ray.collider.gameObject;
                Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * ray.distance, Color.white);
                return true;
            }
        }
        return false;
    }
    void SightRay()
    {
        //----------ray bound----------//
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, -25) * this.gameObject.transform.right * -100, Color.magenta);
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, 25) * this.gameObject.transform.right * -100, Color.magenta);
        //----------ray bound----------//
        Physics2D.Raycast(this.transform.position, Quaternion.Euler(0, 0, 25 - time % 25) * this.gameObject.transform.right * -100, cf2d, lRay, info.detectionLength);
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, 25 - time % 25) * this.gameObject.transform.right * -100, Color.red);
        
        Physics2D.Raycast(this.transform.position, Quaternion.Euler(0, 0, time % 25 - 25) * this.gameObject.transform.right * -100, cf2d, rRay, info.detectionLength);
        Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, time % 25 - 25) * this.gameObject.transform.right * -100, Color.blue);
        if (RayHit(lRay)||RayHit(rRay))
        {
            targPos = targ.transform.position;
        }
    }
}
