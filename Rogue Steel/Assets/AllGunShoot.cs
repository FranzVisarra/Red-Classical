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
    public GameObject pro;
    private GameObject proClone;
    public CannonInfo info;
    //damage for projectiles
    public int Dam;
    public int Pen;
    public float Ang;
    // Start is called before the first frame update
    void Start()
    {
        Dam = 5;
        Pen = 5;
        Ang = 0;
        //mcns = GameObject.Find("Mechanics");
        //sound = GameObject.Find("Sound Cue");
    }
    void FixedUpdate()
    {
        reload();
    }
    private void Update()
    {
        if (info.shootStatus == "Shoot")
        {
            fire();
            info.tSinceShot = 0;
            info.shootStatus = "Reload";
        }
        //fire test
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            fire();
        }
    }
    private void fire()
    {
        //Debug.Log("Shoot Script Says " + transform.position);
        //ptile.fire(10, transform.rotation, transform.position, 10, info.ColLay);
        proClone = Instantiate(pro, transform.position, transform.rotation);
        proClone.GetComponent<Rigidbody2D>().rotation += 90;
        //Debug.Log("shoting pro");
        proClone.layer = info.ColLay;
        proClone.GetComponent<ProStats>().SetPro("Shell",5,5,0,50,3);
        var ProPos = proClone.transform.position;
        proClone.transform.position = new Vector3(ProPos.x, ProPos.y, -3);

        if (this.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            soundClone = Instantiate(sound, transform.position, transform.rotation);
            bangScript = soundClone.GetComponent<AllSndHan>();
            bangScript.type = "Grow";
            bangScript.growSize = 10;
            bangScript.maxSize = 30;
        }
    }
    private void reload()
    {
        if (info.tSinceShot < info.reloadTime)
        {
            info.tSinceShot += Time.deltaTime;
        }
        else if (info.tSinceShot >= info.reloadTime)
        {
            info.shootStatus = "Ready";
        }
    }
}
