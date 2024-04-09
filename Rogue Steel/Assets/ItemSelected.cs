using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelected : MonoBehaviour
{
    //selected gameobject
    public GameObject selectedGameObject;
    public bool selected;
    public string type;
    public GameObject Inventory;
    public InventoryHandling hand;
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        hand = Inventory.GetComponent<InventoryHandling>();
    }
    public void SetSelected(bool boo, GameObject game)
    {
        //check for previous selected and set it as unselected
        if (selected)//don't wanna call a component from a null
        {
            //notable that its only executed when selected is true
            selectedGameObject.GetComponent<SelectItem>().selected = false;
        }
        //set values
        selected = boo;
        selectedGameObject = game;
        //update inventory checker to verify if both are true
        switch (type)
        {
            case "tank":
                hand.tank = boo;
                break;
            case "inventory":
                hand.inv = boo;
                break;
        }
        //ivoke transfer method in inventory handling
        hand.Transfer();
    }
}
