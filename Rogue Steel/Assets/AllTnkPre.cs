using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTnkPre : MonoBehaviour
{
    public string[,] PlTank;
    //public string[,] Rcvd = new string[,] { { "cd", "cg", "am", "en" }, { "ar", "cl", "hd", "fu" } };
    public string[,] Rcvd = new string[,] { { "cd", "ar" }, { "cg", "cl" }, { "am", "hd" }, { "en", "fu" } };
    // Start is called before the first frame update
    void Start()
    {
        PlTank = Rcvd;
    }
}
