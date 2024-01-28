using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class ProMov : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public Collider2D cd;
    // Start is called before the first frame update
    public Vector3 nextPos;
    public List<RaycastHit2D> rc = new List<RaycastHit2D>();
    public ProColHan pch;
    public ContactFilter2D cf2d;
    public LayerMask hitmask;
    int tint;
    void Awake()
    {
        Speed = this.GetComponent<ProStats>().Speed;
        rb.rotation += 90;
        //this.gameObject.transform.position.z = -1;
        //rb.AddRelativeForce(Vector2.up * Speed);
        cf2d.layerMask = hitmask;
        tint = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(Vector2.up * Speed * Time.deltaTime);
        nextPos.x = this.transform.forward.x * Speed;
        nextPos.y = this.transform.forward.y * Speed;
        nextPos.z = this.transform.forward.z;
        //Physics2D.Raycast(rb.position, transform.TransformDirection(Vector2.up * 100f * Time.deltaTime), cf2d, rc, 1000f/*Vector2.Distance(rb.position,nextPos)*/);
        Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.up * Speed * Time.deltaTime), cf2d, rc, Speed*Time.deltaTime/*Vector2.Distance(rb.position,nextPos)*/);
        if (rc!=null)
        {
            //Debug.Log("Test" + tint);
            foreach(RaycastHit2D ray in rc)
            {
                if (ray.collider.gameObject.layer == this.gameObject.layer && ray.collider.gameObject.name != "ProjectileThing(Clone)")
                {
                    Debug.Log("hit " + ray.collider.name + " on layer " + ray.collider.transform.gameObject.layer + " with distance " + ray.distance);
                    Debug.Log(ray.point);
                    pch.RayCastHit(ray);
                }
            }
            //Debug.Log("Test" + tint);
            //tint++;
            //Debug.Log("hit "+rc.collider.name +" on layer " +rc.collider.transform.gameObject.layer);
            //pch.RayCastHit(rc.transform.gameObject);
        }
        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.up * Speed * Time.deltaTime), Color.yellow);
        //transform.position += -transform.right * Speed * Time.deltaTime;
    }
    
}
