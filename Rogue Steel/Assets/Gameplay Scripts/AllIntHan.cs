using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllIntHan : MonoBehaviour
{
    public GameObject mcns;
    public UIHandling uih;
    public GameObject player;
    public AllTnkStats stats;
    public bool playerInRadius;
    public string type;
    public float time;
    public float limit;
    public float amount;
    public Collider2D cd;
    public Startup str;
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
            Debug.Log("Collider Colliding with " + collision.gameObject.name);
            if (time >= limit)
            {
                switch (type)
                {
                    case "Fuel":
                        stats.AddFuel(5*Time.deltaTime);
                        break;
                    case "Crate":
                        //TODO alert nearby enemies
                        uih.UpdateObjectivesDisplay("cap crate");
                        Destroy(this.transform.gameObject);
                        break;
                    case "Spawn":
                        if (str.extract)
                        {
                            str.Win();
                        }
                        break;
                }
            }
            else
            {
                time += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collider Exited with " + collision.gameObject.name);
            playerInRadius = false;
            time = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        str = mcns.transform.GetComponent<Startup>();
        player = GameObject.Find("Player(Clone)");
        player = player.transform.Find("Chassis").gameObject;
        stats = player.GetComponent<AllTnkStats>();
        uih = mcns.GetComponent<UIHandling>();
        time = 0;
        playerInRadius = false;
    }
}
