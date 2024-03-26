using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrdR : MonoBehaviour
{
    public float movsp;
    public Rigidbody2D rb;
    public AllTnkStats stats;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        movsp = 5f;
        stats = transform.parent.Find("Chassis").GetComponent<AllTnkStats>();
    }
    public void Movement(string movType)
    {
        if (stats.driverStatus)
        {
            switch (movType)
            {
                case "Forward":
                    rb.AddRelativeForce(Vector2.up * movsp);
                    break;
                case "Forward Right":
                    rb.AddRelativeForce(Vector2.up * movsp / 2);
                    break;
                case "Forward Left":
                    rb.AddRelativeForce(Vector2.up * 2 * movsp);
                    break;
                case "Back":
                    rb.AddRelativeForce(Vector2.down * movsp);
                    break;
                case "Back Right":
                    rb.AddRelativeForce(Vector2.down * movsp / 2);
                    break;
                case "Back Left":
                    rb.AddRelativeForce(Vector2.down * 2 * movsp);
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
