using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInfo : MonoBehaviour
{
    public int ParLay = 0;
    private GameObject Par;
    public int ProLay = 0;
    // Start is called before the first frame update
    void Start()
    {
        //set parent
        Par = this.transform.parent.gameObject;
        //check parent layer which is horizontal drive
        if (Par.layer == LayerMask.NameToLayer("PlayerCollision"))
        {
            ParLay = LayerMask.NameToLayer("Player");
            //collide with enemy
            ProLay = LayerMask.NameToLayer("EnemyCollision");
        }
        else if (Par.layer == LayerMask.NameToLayer("EnemyCollision"))
        {
            ParLay = LayerMask.NameToLayer("Enemy");
            //collide with player
            ProLay = LayerMask.NameToLayer("PlayerCollision");
        }
        //set layer
        this.transform.gameObject.layer = ParLay;
    }
}
