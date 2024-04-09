using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory
{
    public string type;
    public string name;
    public int amount;
    public inventory(string type, string name, int amount)
    {
        this.type = type;
        this.name = name;   
        this.amount = amount;
    }
}
public class InventoryHandling : MonoBehaviour
{
    public string StartMode;
    public GameObject TankGrid;
    public GameObject InventoryGrid;
    public GameObject Cell;
    public List<inventory> inventory;
    public InventoryCellSlot slot;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<inventory>();
        switch (StartMode)
        {
            case "new":
                inventory.Add(new inventory("cd","",1));
                inventory.Add(new inventory("cg", "", 1));
                inventory.Add(new inventory("cl", "", 1));
                inventory.Add(new inventory("en", "Basic", 1));
                inventory.Add(new inventory("hd", "Basic Svarsky 30mm", 1));
                inventory.Add(new inventory("am", "Basic 30mm", 1));
                inventory.Add(new inventory("fu", "Small", 2));
                break;
            case "load":
                //load Data
                break;
        }
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        foreach (GameObject g in InventoryGrid.transform.Find(""))
        {
            Destroy(g.gameObject);
        }
        foreach (inventory item in inventory)
        {
            GameObject temp = Instantiate(Cell, InventoryGrid.transform);
            temp.AddComponent<InventoryCellSlot>();
        }
    }
}
