using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInfo : MonoBehaviour
{
    public GameObject player;
    public AllTnkStats playerstatsref;
    string type;
    string variant;
    //----------item specific stats----------//
    string caliber;
    int amount;
    //----------item specific stats----------//
    float time;
    bool playerInRadius;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        player = player.transform.Find("Chassis").gameObject;
        playerstatsref = player.GetComponent<AllTnkStats>();
        //----------item specific stats----------//
        //TODO make item drops random
        type = "Pro";
        variant = "30mm APC";
        caliber = "30mm";
        amount = 200;
        //----------item specific stats----------//
        time = 0;
        playerInRadius = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collider Entered with " + collision.gameObject.name);
            playerInRadius = true;
            time = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (time >= 3)
            {
                //TODO add item to inventory
                switch (type)
                {
                    case "Pro":
                        playerstatsref.AddAmmo(variant, caliber, amount);
                        Destroy(this.transform.gameObject);
                        break;
                }
            }
            time += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collider Exited with " + collision.gameObject.name);
            time = 0;
            playerInRadius = false;
        }
    }
}
