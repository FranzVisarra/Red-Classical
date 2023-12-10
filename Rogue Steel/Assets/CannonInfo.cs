using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInfo : MonoBehaviour
{
    public int ParLay = 0;
    private GameObject Par;
    // Start is called before the first frame update
    void Start()
    {
        //set parent
        Par = this.transform.parent.gameObject;
        //check parent layer which is horizontal drive
        if (Par.layer == LayerMask.NameToLayer("PlayerCollision"))
        {
            ParLay = LayerMask.NameToLayer("Player");
        }
        else if (Par.layer == LayerMask.NameToLayer("EnemyCollision"))
        {
            ParLay = LayerMask.NameToLayer("Enemy");
        }
        //set layer
        this.transform.gameObject.layer = ParLay;
    }
}
