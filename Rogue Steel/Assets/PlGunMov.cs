using System.Collections.Generic;
using UnityEngine;

//handles player gun movement and player hun detection
public class PlGunMov : MonoBehaviour
{
    public GameObject mcns;
    public GameObject chassis;
    public CannonInfo info;
    public Projectile ptile;
    public float moveSpeed;
    public float rotateSpeed;
    public Camera cam;
    Vector2 mousePos;
    public Vector2 targPos;
    public Vector2 curPos;
    public Quaternion targRot;
    public float angle;
    public string rotate;

    //public GameObject test;
    //public GameObject testRef;

    public List<RaycastHit2D> rc = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            chassis = this.transform.parent.gameObject;
            if (chassis.name == "Chassis")
            {
                break;
            }
        }
        
        mcns = GameObject.Find("Mechanics");
        cam = Camera.main;
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        //move cannon rotation point
        //transform.Translate(1, 0, 0);
        //testRef = Instantiate(test);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(this.GetType().ToString() + " FixedUpdate Start");
        DetectTarget();
        //Debug.Log(this.GetType().ToString() + " FixedUpdate End");
    }

    private void DetectTarget()
    {
        Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), cf2d, rc,info.detectionLength);
        Debug.DrawRay(this.transform.position, transform.right * -info.detectionLength, Color.magenta);
        foreach (RaycastHit2D ray in rc)
        {
            if (ray.collider.transform.gameObject.layer==LayerMask.NameToLayer("Enemy"))
            {
                Debug.DrawRay(this.transform.position, transform.right * -ray.distance, Color.cyan);
                Debug.Log("Enemy in Sights");
                if(info.shootStatus=="Ready")
                { info.shootStatus = "Shoot"; }
                break;
            }
        }
    }

    void Update()
    {
        //Debug.Log(this.GetType().ToString() + " Update Start");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rotate = "Direction";
                targPos.x = (curPos.x-mousePos.x);
                targPos.y = (curPos.y-mousePos.y);
            }
            else
            {
                rotate = "Point";
                targPos = mousePos;
            }
            //testRef.transform.position = targPos;
        }
        curPos.Set(transform.position.x, transform.position.y);
        if (rotate == "Direction")
        {
            angle = Mathf.Atan2(targPos.y, targPos.x) * Mathf.Rad2Deg;
            targRot = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.deltaTime);
        }
        else if (rotate == "Point")
        {
            angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
            targRot = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.deltaTime);
        }
        //Debug.Log(this.GetType().ToString() + " Update End");
    }
}
