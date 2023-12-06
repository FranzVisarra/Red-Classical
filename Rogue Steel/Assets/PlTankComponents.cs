using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlTankComponents : MonoBehaviour
{
    public string[,] PlTank;
    public string[,] Rcvd =new string[2,4]{{"cd","cg","am","en"},{"ar","cl","hd","fu"} };
    // Start is called before the first frame update
    void Start()
    {
        PlTank = Rcvd;
    }
}
