using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject Player;
    private GameObject InstPlay;
    public string[,] Rcvd = new string[,] { { "cd", "ar" }, { "cg", "cl" }, { "am", "hd" }, { "en", "fu" } };
    // Start is called before the first frame update
    void Start()
    {
        //load player
        InstPlay = Instantiate(Player);
        InstPlay.GetComponentInChildren<AllBodCom>().innards = Rcvd;
        //load map

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}