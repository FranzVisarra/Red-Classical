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
    public RaycastHit2D rc;
    public ProColHan pch;
    public ContactFilter2D cf2d;
    public LayerMask hitmask;
    void Awake()
    {
        Speed = this.GetComponent<ProStats>().Velocity;
        rb.rotation += 90;
        //this.gameObject.transform.position.z = -1;
        //rb.AddRelativeForce(Vector2.up * Speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        nextPos.x = rb.transform.forward.x*Speed;
        nextPos.y = rb.transform.forward.y*Speed;
        nextPos.z = rb.transform.forward.z;
        //Physics2D.Raycast(rb.position, transform.TransformDirection(Vector2.up * 100f * Time.deltaTime), cf2d, rc, 1000f/*Vector2.Distance(rb.position,nextPos)*/);
        rc=Physics2D.Raycast(rb.position, transform.TransformDirection(Vector2.up * Speed * Time.deltaTime), Speed*Time.deltaTime/*Vector2.Distance(rb.position,nextPos)*/,hitmask);
        if (rc.collider!=null)
        {
            Debug.Log("hit "+rc.collider.name +" on layer " +rc.collider.transform.gameObject.layer);
            //pch.RayCastHit(rc.transform.gameObject);
        }
        Debug.DrawRay(rb.position, transform.TransformDirection(Vector3.up * 100f), Color.yellow);
        rb.transform.Translate(Vector2.up * Speed * Time.deltaTime);
        //transform.position += -transform.right * Speed * Time.deltaTime;
    }
    
}
