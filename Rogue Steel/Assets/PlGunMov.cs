using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlGunMov : MonoBehaviour
{
    public GameObject mcns;
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

    public List<RaycastHit2D> rc = new List<RaycastHit2D>();
    public ContactFilter2D cf2d;
    // Start is called before the first frame update
    void Start()
    {
        mcns = GameObject.Find("Mechanics");
        cam = Camera.main;
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        //move cannon rotation point
        //transform.Translate(1, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectTarget();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targPos.x = (mousePos.x - curPos.x) * 100 + curPos.x;
                targPos.y = (mousePos.y - curPos.y) * 100 + curPos.y;
            }
            else
            {
                targPos = mousePos;
            }
        }
        curPos.Set(transform.position.x, transform.position.y);
        
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
                if(info.shootStatus=="Ready")
                { info.shootStatus = "Shoot"; }
                break;
            }
        }
    }

    void Update()
    {   angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
        targRot = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.deltaTime);
    }
}
