using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObsHan : MonoBehaviour
{
    public Collider2D cd;
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject temp = Instantiate(sound,new Vector3(this.transform.position.x, this.transform.position.y,-1),new Quaternion());
            temp.transform.GetComponent<AllSndHan>().maxSize = 10;
            temp.transform.GetComponent<AllSndHan>().growSize = 5;

            Destroy(this.transform.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(this.transform.gameObject);
        }
        Debug.Log(collision.name);
    }
}
