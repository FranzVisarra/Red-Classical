using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public Rigidbody2D rb;
    float accelerationPower = 5f;
    float steeringPower = 5f;
    float steeringAmount, speed, direction;
    public float dirDeg;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        steeringAmount = -Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical") * accelerationPower;
        direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up)));
        rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;
        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmount / 2);
        dirDeg = this.transform.eulerAngles.z;
    }
}
