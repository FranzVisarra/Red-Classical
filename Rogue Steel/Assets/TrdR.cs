using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrdR : MonoBehaviour
{
    public float movsp;
    public Rigidbody2D rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        movsp = 5f;
    }
    public void Movement(string movType)
    {
        switch (movType)
        {
            case "Forward":
                rb.AddRelativeForce(Vector2.up * movsp);
                break;
            case "Forward Right":
                rb.AddRelativeForce(Vector2.up * movsp/2);
                break;
            case "Forward Left":
                rb.AddRelativeForce(Vector2.up * 2* movsp);
                break;
            case "Back":
                // Code
                break;
            case "Back Right":
                // Code
                break;
            case "Back Left":
                // Code
                break;
            case "Rotate Right":
                rb.AddRelativeForce(Vector2.down * movsp);
                break;
            case "Rotate Left":
                rb.AddRelativeForce(Vector2.up * movsp);
                break;
            default:
                // Code
                break;
        }
        /*
        if (movType == "Forward")
        {
            Debug.Log("Force Added");
            rb.AddRelativeForce(Vector2.up * movsp);
        }
        else if (movType == "RotRight")
        {
            Debug.Log("Force Added");
            rb.AddRelativeForce(Vector2.down * movsp);
        }
        else if (movType == "RotLeft")
        {
            Debug.Log("Force Added");
            rb.AddRelativeForce(Vector2.up * movsp);
        }
        */
    }
}
