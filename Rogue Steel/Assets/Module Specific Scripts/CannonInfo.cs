using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonInfo : MonoBehaviour
{
    public int ParLay = 0;
    private GameObject GranPar;
    public int ColLay;
    public PlGunMov pl;
    public EnGunMov en;
    //gun stats
    public int shellVelocity;
    public int shellPen;
    public int shellSpeed;
    public string shootStatus;
    public float tSinceShot;
    public float reloadTime;
    public float detectionLength;
    public string caliber;
    public bool hasRound;

    private void Awake()
    {
        Debug.Log("Cannon Info Awake");
        GranPar = this.transform.parent.parent.gameObject;
        hasRound = false;
        //establish layer
        //check parent layer which is horizontal drive
        if (GranPar.layer == LayerMask.NameToLayer("Player"))
        {
            ParLay = LayerMask.NameToLayer("Player");
            ColLay = LayerMask.NameToLayer("EnemyCollision");
            Destroy(en);
            //Debug.Log("Destroyed Enemy Gun Script");
        }
        else if (GranPar.layer == LayerMask.NameToLayer("Enemy"))
        {
            ParLay = LayerMask.NameToLayer("Enemy");
            ColLay = LayerMask.NameToLayer("PlayerCollision");
            Destroy(pl);
        }
        //set layer
        this.transform.gameObject.layer = ParLay;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Cannon Info Start");
        //establish layer of projectiles
        shootStatus = "Reload";
        tSinceShot = 0;
        detectionLength = 100;
    }
    public void setStats(float reloadTime, string caliber)
    {
        this.reloadTime = reloadTime;
        this.caliber = caliber;
    }
}
