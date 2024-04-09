using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour
{
    //selected gameobject
    public GameObject selectedGameObject;
    public bool selected;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }
    //invoke before setting selected gameobject
    public void UnSelectPrevious()
    {
        if (selected)//don't wanna call a component from a null
        {
            selectedGameObject.GetComponent<SelectItem>().selected = false;
        }
    }
}
