using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlTankComp : MonoBehaviour
{
    public string[,] PlTank;
    public string[,] Rcvd = new string[,] { { "cd", "cg", "am", "en" }, { "ar", "cl", "hd", "fu" } };
    // Start is called before the first frame update
    void Start()
    {
        PlTank = Rcvd;
    }
}
