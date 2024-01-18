using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject InstEnemy;
    public string[,] Rcvd = new string[,] { { "cd", "ar" }, { "cg", "cl" }, { "am", "hd" }, { "en", "fu" } };
    // Start is called before the first frame update
    void Start()
    {
        InstEnemy = Instantiate(Enemy);
        InstEnemy.GetComponentInChildren<AllBodCom>().innards = Rcvd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
