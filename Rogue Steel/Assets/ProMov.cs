using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class ProMov : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public Collider2D cd;
    // Start is called before the first frame update
    public List<RaycastHit2D> rc = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;
    public LayerMask hitmask;
    public ProStats ps;
    public ProColHan pc;
    void Awake()
    {
        rb.rotation += 90;
        cf2d.layerMask = hitmask;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Physics2D.Raycast(rb.position, transform.TransformDirection(Vector2.up * 100f * Time.deltaTime), cf2d, rc, 1000f/*Vector2.Distance(rb.position,nextPos)*/);
        Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.up * Speed * Time.deltaTime), cf2d, rc, Speed*Time.deltaTime);
        if (rc.Any())//if ray hits
        {
            //Debug.Log("Ray Hit Something");
            int legalCast = 0;
            foreach (RaycastHit2D ray in rc)
            {
                //Debug.Log("Object Hit Tag : " + ray.collider.gameObject.tag);
                //Debug.Log("Projectile Tag Filter : " + ps.ProHit);
                if (ps.ProHit.Contains(ray.collider.gameObject.tag) && ray.distance >= 0.01)//filter for tag and make sure that distance isn't infinitessamly small to avoid locking transform
                {
                    legalCast++;
                    //Debug.Log("hit " + ray.collider.name + " on layer " + ray.collider.transform.gameObject.layer + " with distance " + ray.distance);
                    //Debug.Log("Filtered");
                    pc.RayCastHit(ray);
                    this.transform.Translate(Vector2.up * ray.distance);
                    //Debug.Log("transformed to " + this.transform.position);
                    break;
                }
            }
                if(legalCast == 0)//no raycasts hit valid
                {
                    this.transform.Translate(Vector2.up * Speed * Time.deltaTime);
                }
        }
        else//ray hits nothing
        {
            this.transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
    }
    
}
