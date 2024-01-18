using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllGunShoot : MonoBehaviour
{
    public GameObject mcns;
    public Projectile ptile;
    //public CannonInfo info;
    public int layer;
    public int ColLay;
    // Start is called before the first frame update
    void Start()
    {
        mcns = GameObject.Find("Mechanics");
        Debug.Log("Layer = " + this.transform.gameObject.layer);
        layer = this.transform.gameObject.layer;
        Debug.Log("Geting Projectile");
        ptile = mcns.GetComponent<Projectile>();
        Debug.Log("Got");
        if (layer == LayerMask.NameToLayer("Player"))
        {
            ColLay = LayerMask.NameToLayer("EnemyCollision");
            this.AddComponent<PlGunMov>();
        }
        else if (layer == LayerMask.NameToLayer("Enemy"))
        {
            ColLay = LayerMask.NameToLayer("PlayerCollision");
        }
        Debug.Log("ColLay = "+ColLay);
    }

    // Update is called once per frame
    void Update()
    {
        //fire test
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Shoot Script Says "+transform.position);
            ptile.fire(10, transform.rotation, transform.position, 10, ColLay);
        }
    }
}
