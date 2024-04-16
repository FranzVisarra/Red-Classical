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
    public bool tank;
    public bool inv;

    // Start is called before the first frame update
    void Start()
    {
        tank = false;
        inv = false;
        inventory = new List<inventory>();
        switch (StartMode)
        {
            case "new":
                inventory.Add(new inventory("cd","Basic",1));
                inventory.Add(new inventory("cg", "Basic", 1));
                inventory.Add(new inventory("cl", "Basic", 1));
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
        foreach (Transform g in InventoryGrid.transform.Find(""))
        {
            Destroy(g.gameObject);
        }
        foreach (inventory item in inventory)
        {
            GameObject temp = Instantiate(Cell, InventoryGrid.transform);
            temp.name = item.type;
            InventoryCellSlot temptemp = temp.AddComponent<InventoryCellSlot>();
            temptemp.SetValues(item.type, item.name, item.amount);
            temp.GetComponent<SelectItem>().inv = temptemp;
            temp.GetComponent<SelectItem>().Inventory = this.transform.gameObject;
        }
    }
    //check if an item from both are selected then transfer
    //TODO this is a very problem area
    public void Transfer()
    {
        if(tank && inv)
        {
            Debug.Log("Transfer Start");
            //transfer item to slot
            //decrement amount
            string tempType = InventoryGrid.GetComponent<ItemSelected>().selectedGameObject.name;
            string tempName = InventoryGrid.GetComponent<ItemSelected>().selectedGameObject.GetComponent<InventoryCellSlot>().moniker;
            foreach (var item in inventory)
            {
                if (item.type == tempType && item.name == tempName)
                {
                    if (item.amount > 0)
                    {
                        //decrement inventroy
                        item.amount--;
                        //fill tank 
                        TankGrid.GetComponent<ItemSelected>().selectedGameObject.GetComponent<TankCellSlot>().SetValues(tempType, tempName);
                        TankGrid.GetComponent<ItemSelected>().selectedGameObject.GetComponent<SelectItem>().filled = true;
                        //clear InventoryGrid Selected fore reset
                        InventoryGrid.GetComponent<ItemSelected>().selected = false;
                        InventoryGrid.GetComponent<ItemSelected>().SetSelected(false,null);
                        UpdateInventory();
                    }
                    break;
                }
            }
            //clear TankGrid Selected for reset
            TankGrid.GetComponent<ItemSelected>().selected = false;
            TankGrid.GetComponent<ItemSelected>().SetSelected(false, null);
            Debug.Log("Transfer End");
        }
    }
    public void RemoveFromInventory(string name, string type)
    {
        Debug.Log("Removing from inventory");
        foreach (inventory item in inventory)
        {
            if (name == item.name && type == item.type)
            {
                item.amount--;
                break;
            }
        }
        UpdateInventory();
    }
    public void AddToInventory(string name, string type)
    {
        Debug.Log("Adding to inventory");
        foreach (inventory item in inventory)
        {
            if (name == item.name && type == item.type)
            {
                item.amount++;
                break;
            }
        }
        UpdateInventory();
    }
}
