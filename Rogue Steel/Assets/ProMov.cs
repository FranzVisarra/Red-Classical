using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ProMov : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Speed = this.GetComponent<ProStats>().Speed;
        rb.rotation += 90;
        rb.AddRelativeForce(Vector2.up * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position += -transform.right * Speed * Time.deltaTime;
    }
}
