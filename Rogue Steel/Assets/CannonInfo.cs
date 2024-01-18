using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonInfo : MonoBehaviour
{
    public int ParLay = 0;
    private GameObject Par;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CannonStart");
        this.AddComponent<AllGunShoot>();
        //set parent
        Par = this.transform.parent.gameObject;
        //check parent layer which is horizontal drive
        if (Par.layer == LayerMask.NameToLayer("PlayerCollision"))
        {
            ParLay = LayerMask.NameToLayer("Player");
            this.AddComponent<PlGunMov>();
        }
        else if (Par.layer == LayerMask.NameToLayer("EnemyCollision"))
        {
            ParLay = LayerMask.NameToLayer("Enemy");
            this.AddComponent<EnGunMov>();
        }
        //set layer
        this.transform.gameObject.layer = ParLay;
        Debug.Log("CannonEnd");
    }
}
