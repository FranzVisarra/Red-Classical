using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AllGunShoot : MonoBehaviour
{
    public GameObject mcns;
    public ItemTable items;
    public GameObject sound;
    public AllSndHan bangScript;
    private GameObject soundClone;
    public GameObject pro;
    private GameObject proClone;
    public CannonInfo info;
    //damage for projectiles
    public int dam;
    public int pen;
    public float ang;
    public float speed;
    public float lifeTime;
    public string round;
    
    public GameObject Par;
    public AllTnkStats stats;
    //this represents the number of caliber we are using
    public int ammoCaliberNumber;//between 1 and count
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AllGunShoot Start");
        Par = this.transform.parent.parent.gameObject;
        stats = Par.GetComponent<AllTnkStats>();

        ammoCaliberNumber = stats.CountAmmoCalibers(info.caliber);
        items = mcns.GetComponent<ItemTable>();
        //mcns = GameObject.Find("Mechanics");
        //sound = GameObject.Find("Sound Cue");
        foreach (var ammo in stats.storage)
        {
            if (ammo.caliber == info.caliber)
            {
                round = ammo.name;
                SetProjectileStats(round);
                info.shootStatus = "Reload";
                info.tSinceShot = 0;
                if (ammo.amount > 0)
                {
                    stats.RemoveAmmo(round, 1);
                    info.hasRound = true;
                }
                break;
            }
        }
    }
    private void Update()
    {
        //TODO make default rounds appear because enemy tank also requires tempering
        
        //cycle through ammo types for caliber
        //TODO put this on a player thing
        if (Input.GetKeyDown(KeyCode.R) && this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //check if round is currently being loaded
            if (info.hasRound)
            {
                stats.AddAmmo(round,info.caliber,1);
                info.hasRound = false;
            }
            ammoCaliberNumber++;//choose the next caliber in sequence
            //check overflow
            if (ammoCaliberNumber > stats.CountAmmoCalibers(info.caliber))
            {
                //control overflow
                ammoCaliberNumber = 1;
            }
            int calibersCounted = 0;
            foreach (var ammo in stats.storage)
            {
                if (ammo.caliber == info.caliber)
                {
                    calibersCounted++;
                    //check if calibers counted in storage is equal to the number we are looking for
                    if (calibersCounted == ammoCaliberNumber)
                    {
                        round = ammo.name;
                        Debug.Log(round + " CHOSEN");
                        SetProjectileStats(round);
                        info.shootStatus = "Reload";
                        info.tSinceShot = 0;
                        //check if round of type has ammo
                        if (ammo.amount > 0)
                        {
                            stats.RemoveAmmo(round, 1);
                            info.hasRound = true;
                        }
                        //round found so break out of loop
                        break;
                    }
                }
            }
        }

        if (info.shootStatus == "Shoot")
        {
            fire();
            info.tSinceShot = 0;
            info.shootStatus = "Reload";
            info.hasRound = false;
            if (stats.CheckAmmoExistsInStorageByName(round))
            {
                stats.RemoveAmmo(round, 1);
                info.hasRound = true;
            }
        }
        /*fire test
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            fire();
        }
        */
        Reloading();
    }
    private void fire()
    {
        //Debug.Log("Shoot Script Says " + transform.position);
        //ptile.fire(10, transform.rotation, transform.position, 10, info.ColLay);
        proClone = Instantiate(pro, transform.position, transform.rotation);
        //proClone.GetComponent<Rigidbody2D>().rotation += 90;
        //Debug.Log("shoting pro");
        proClone.layer = info.ColLay;
        proClone.GetComponent<ProStats>().SetPro("Shell",dam,pen,ang,50,3);
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
    private void Reloading()
    {
        switch (info.hasRound)
        {
            case true:
                if (info.tSinceShot < info.reloadTime)
                {
                    info.tSinceShot += Time.deltaTime;
                    //Debug.Log(info.reloadTime - info.tSinceShot + " seconds left");
                }
                else if (info.tSinceShot >= info.reloadTime)
                {
                    info.shootStatus = "Ready";
                    //Debug.Log("Ready to Fire");
                }
                break;
            case false:
                //do nothing
                break;
        }
    }
    private void SetProjectileStats(string round)
    {
        foreach (var pro in items.pro)
        {
            if (round == pro.name)
            {
                dam = pro.Dam;
                pen = pro.Pen;
                ang = pro.Ang;
                speed = pro.Speed;
                lifeTime = pro.LifeTime;
                break;
            }
        }
    }
}
