using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewInfo : MonoBehaviour
{
    public GameObject Par;
    public GameObject Engine;
    public GameObject Cannon;
    public AllTnkStats stats;
    public string CrewType;
    public bool manned;//whether or not this component has a user
    
    void Start()
    {
        Par = this.transform.parent.gameObject;
        Engine = Par.transform.Find("Engine(Clone)").gameObject;
        stats = Par.GetComponent<AllTnkStats>();
        switch (CrewType)
        {
            case "Driver":
                stats.driverStatus = true;
                break;
            case "Gunner":
                stats.gunnerStatus = true;
                break;
            case "Loader":
                stats.loaderStatus = true;
                break;
        }
    }
    public void Destroyed()
    {
        switch(CrewType)
        {
            case "Driver":
                stats.driverStatus = false;
                break;
            case "Gunner":
                stats.gunnerStatus = false;
                break;
            case "Loader":
                stats.loaderStatus = false;
                break;
        }
    }
}
