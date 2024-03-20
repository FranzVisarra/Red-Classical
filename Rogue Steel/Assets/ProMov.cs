using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//this class concerns movement and raycast
public class ProMov : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public Collider2D cd;
    // Start is called before the first frame update
    public List<RaycastHit2D> rc = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;
    public ProStats ps;
    public ProColHan pc;
    void Start()
    {
        //Debug.Log("Setting layyer from pro "+LayerMask.LayerToName(this.gameObject.layer));
        cf2d.layerMask |= (1 << this.gameObject.layer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " FixedUpdate Start");
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
                    var DRC = Color.gray;//debug ray color
                    pc.RayCastHit(ray);
                    if (ps.ProHit.Contains("Armor") && ps.ProHit.Contains("Module"))
                    {
                        DRC = Color.blue;
                    }
                    else if (ps.ProHit.Contains("Armor"))
                    {
                        DRC = Color.red;
                    }
                    else if (ps.ProHit.Contains("Module"))
                    {
                        DRC = Color.yellow;
                    }
                    Debug.DrawRay(rb.transform.position, transform.up * ray.distance, DRC, 1);
                    this.transform.Translate(Vector2.up * ray.distance);
                    //Debug.Log("transformed to " + this.transform.position);
                    break;
                }
            }
                if(legalCast == 0)//no raycasts hit valid
            {
                Debug.DrawRay(rb.transform.position, transform.up * Speed * Time.deltaTime, new Color(1,1,1,1/10), 1);
                this.transform.Translate(Vector2.up * Speed * Time.deltaTime);
            }
        }
        else//ray hits nothing
        {
            Debug.DrawRay(rb.transform.position, transform.up * Speed * Time.deltaTime, Color.gray, 1);
            this.transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
        //Debug.Log(this.GetType().ToString() + " FixedUpdate End");
    }
    
}
