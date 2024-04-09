using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandling : MonoBehaviour
{
    public string StartMode;

    // Start is called before the first frame update
    void Start()
    {
        switch (StartMode)
        {
            case "new":

                break;
            case "load":
                //load Data
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
