using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSndHan : MonoBehaviour
{
    public string type;
    public float curSize;
    public float growSize;
    public float maxSize;
    public CircleCollider2D cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = this.GetComponent<CircleCollider2D>();
        curSize = 0;
        //maxSize = 1;
        //growSize = 1;

        //size = 1;
        //type = "";

        this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y, -1); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (type == "Stagnant")
        {
            cc.radius = curSize;
        }
        else if (type == "Grow")
        {
            //Debug.Log("Size = "+curSize);
            if (curSize >= maxSize)
            {
                Destroy(this.transform.gameObject);
            }
            curSize += growSize*Time.fixedDeltaTime;
        }
        this.gameObject.transform.localScale= new Vector3(curSize, curSize, 0);
    }
}
