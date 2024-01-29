using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllGunShoot : MonoBehaviour
{
    public GameObject mcns;
    public GameObject sound;
    public AllSndHan bangScript;
    private GameObject soundClone;
    public Projectile ptile;
    public GameObject pro;
    private GameObject proClone;
    public CannonInfo info;
    // Start is called before the first frame update
    void Start()
    {
        //mcns = GameObject.Find("Mechanics");
        //Debug.Log("Geting Projectile");
        ptile = mcns.GetComponent<Projectile>();
        //Debug.Log("Got");
        //sound = GameObject.Find("Sound Cue");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //fire test
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Shoot Script Says "+transform.position);
            //ptile.fire(10, transform.rotation, transform.position, 10, info.ColLay);
            proClone = Instantiate(pro, transform.position, transform.rotation);
            //proClone.layer = info.ColLay;
            proClone.GetComponent<ProStats>().ProType = "AP";
            var ProPos = proClone.transform.position;
            proClone.transform.position = new Vector3(ProPos.x,ProPos.y,-3);
            /*
            soundClone = Instantiate(sound,transform.position,transform.rotation);
            bangScript = soundClone.GetComponent<AllSndHan>();
            bangScript.type = "Grow";
            bangScript.growSize = 10;
            bangScript.maxSize = 30;
            */
        }
    }
}
