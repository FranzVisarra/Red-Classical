using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour
{
    //selected gameobject
    public GameObject selectedGameObject;
    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }
}
