using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrdL : MonoBehaviour
{
    public float movsp;
    public Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        movsp = 1f;
    }
    public void Movement(string mrot, string mdir)
    {
        if (mdir == "Forward")
        {
            Debug.Log("Force Added");
            rb.AddRelativeForce(Vector2.up * movsp);
        }
        if (mrot == "Right")
        {
            Debug.Log("Force Added");
            rb.AddRelativeForce(Vector2.up * movsp);
        }
        else if (mrot == "Left")
        {
            Debug.Log("Force Added");
            rb.AddRelativeForce(Vector2.down * movsp);
        }
    }
}
