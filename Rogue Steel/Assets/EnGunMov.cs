using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    public List<RaycastHit2D> rc = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        targPos = new Vector2(0,0);
        Enemy = this.transform.parent.parent.parent.gameObject;
        EScript = Enemy.GetComponent<EnTnkStats>();
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
        Sight();
        if (EScript.DetState=="Sound Heard")
        {
            targPos = EScript.TestNoiseDirection;
        }
    }
    private void Sight()
    {
        float totDist = info.detectionLength;
        Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), cf2d, rc, info.detectionLength);
        foreach(RaycastHit2D ray in rc)
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
        /*
        for (int i=0; i < 90; i+=5)
        {
            //Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), cf2d, rc, info.detectionLength);
            //Debug.DrawRay(this.transform.position, Quaternion.Euler(0,0,info.detectionAngle/2-i*info.rayAng)*this.gameObject.transform.right*-100, Color.magenta);
            Debug.DrawRay(this.transform.position, Quaternion.Euler(0, 0, 45-i) * this.gameObject.transform.right * -100, Color.magenta);
            Debug.Log("TEST "+ Mathf.Rad2Deg*1/100);
        }
        //Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), cf2d, rc, info.detectionLength);
        //Debug.DrawRay(this.transform.position, transform.right * -info.detectionLength, Color.magenta);
        */
    }
}
