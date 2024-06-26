using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SndColHan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(this.GetType().ToString() + " OnTriggerEnter2D Start");
        //Debug.Log(other.transform.gameObject.layer+" "+other.gameObject.name);
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Debug.Log("Sound Collided With Enemy");
            if (other.gameObject.name == "Enemy Hearing")
            {
                //Debug.Log("Sound Collided with Enemy Chassis");
                other.transform.parent.parent.GetComponent<EnTnkStats>().soundHeard(transform.position);
            }
        }
        //Debug.Log(this.GetType().ToString() + " OnTriggerEnter2D End");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
