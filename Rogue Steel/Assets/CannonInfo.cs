using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonInfo : MonoBehaviour
{
    public int ParLay = 0;
    private GameObject Par;
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

    private void Awake()
    {
        Debug.Log("Cannon Info Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cannon Info Start");
        //establish layer
        Par = this.transform.parent.gameObject;
        //check parent layer which is horizontal drive
        if (Par.layer == LayerMask.NameToLayer("PlayerCollision"))
        {
            ParLay = LayerMask.NameToLayer("Player");
            ColLay = LayerMask.NameToLayer("EnemyCollision");
            Destroy(en);
            Debug.Log("Destroyed Enemy Gun Script");
        }
        else if (Par.layer == LayerMask.NameToLayer("EnemyCollision"))
        {
            ParLay = LayerMask.NameToLayer("Enemy");
            ColLay = LayerMask.NameToLayer("PlayerCollision");
            Destroy(pl);
        }
        //set layer
        this.transform.gameObject.layer = ParLay;
        //establish layer of projectiles
        shootStatus = "Ready";
        reloadTime = 3;
        tSinceShot = reloadTime;
        detectionLength = 100;
    }
}
