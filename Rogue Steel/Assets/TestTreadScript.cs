using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTreadScript : MonoBehaviour
{
    public Rigidbody2D rb;
    float accelerationPower = 10f;
    float steeringPower = 5f;
    float steeringAmount, speed, direction;
    public float dirDeg;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = Input.GetAxis("Vertical") * accelerationPower;
        rb.AddRelativeForce(Vector2.right*speed);
    }
}
