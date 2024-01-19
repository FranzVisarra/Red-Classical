using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnBodMov : MonoBehaviour
{
    public GameObject TLeft;
    public GameObject TRight;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    public void shortRef(string TrackMov)
    {
        TLeft.GetComponent<TrdL>().Movement(TrackMov);
        TRight.GetComponent<TrdR>().Movement(TrackMov);
    }
}
