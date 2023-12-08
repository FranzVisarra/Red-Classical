using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlBodMov : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Transform transform;
    public Camera cam;
    Vector2 mousePos;
    public Vector2 targPos;
    public Vector2 curPos;
    public Quaternion targRot;
    public float angle;
    public Rigidbody2D rb;
    float accelerationPower = 5f;
    float steeringPower = 5f;
    float steeringAmount, speed, direction;
    public float dirDeg;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.5f;
        rotateSpeed = 50f;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            targPos = mousePos;
        }
        curPos.Set(transform.position.x, transform.position.y);
    }

    void FixedUpdate()
    {
        //float angle2 = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg+90;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle2));
        /*
        angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;
        targRot = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targRot, rotateSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(targPos, curPos) > 1)
        {
            transform.position += -transform.right * moveSpeed * Time.fixedDeltaTime;
        }
        */
        /*
        steeringAmount = -Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical") * accelerationPower;
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;
        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);
        */
        dirDeg = this.transform.eulerAngles.z;
        angle = 0-(Mathf.Atan2(transform.position.x - targPos.x, transform.position.y - targPos.y) * Mathf.Rad2Deg);
        if (angle < 0)
        {
            //make angle match eulerangles
            angle += 360;
        }
        /*
        if (dirDeg - angle>0)
        {
            rb.AddTorque(Vector2.up*5, ForceMode2D.Impulse);
        }else if (dirDeg - angle < 0)
        {
            rb.AddTorque(-5, ForceMode2D.Impulse);
        }
        */
        //rb.AddTorque();
        if (Vector2.Distance(targPos, curPos) > 1)
        {
            
        }
        //angle = Mathf.Atan2(transform.position.y - targPos.y, transform.position.x - targPos.x) * Mathf.Rad2Deg;

        
        
    }
}
