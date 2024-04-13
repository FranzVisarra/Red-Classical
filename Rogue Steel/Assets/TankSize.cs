using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TankSize : MonoBehaviour, IPointerClickHandler
{
    public Vector2 size;
    public GameObject tnkpnl;
    public ItemSelected tnksel;
    public GridLayoutGroup tnkSize;
    public GameObject cell;
    public GameObject Inventory;
    private InventoryManager inventoryManager; // Reference to InventoryManager script

    private void Start()
    {
        tnkpnl = GameObject.Find("Tank Panel");
        tnksel = tnkpnl.GetComponent<ItemSelected>();
        tnkSize = tnkpnl.GetComponent<GridLayoutGroup>();

        // Get reference to InventoryManager script
        inventoryManager = Inventory.GetComponent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            foreach (Transform g in tnkpnl.transform.Find(""))
            {
                Destroy(g.gameObject);
            }
            tnkSize.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            tnkSize.constraintCount = (int)size.x;
            inventoryManager.ClearEquippedSlots();
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    GameObject temp = Instantiate(cell, tnkpnl.transform);
                    TankCellSlot temptemp = temp.AddComponent<TankCellSlot>();
                    temptemp.pos = new Vector2(x - (size.x / 2) + 0.5f, y - (size.y / 2) + 0.5f);
                    temp.GetComponent<SelectItem>().tank = temptemp;
                    temp.GetComponent<SelectItem>().Inventory = Inventory;

                    // Add the newly created cell to the EquippedSlots array in InventoryManager

                    inventoryManager.AddEquippedSlot(temp);
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Click on " + gameObject.name);
            foreach (Transform g in tnkpnl.transform.Find(""))
            {
                Destroy(g.gameObject);
                tnkSize.constraint = GridLayoutGroup.Constraint.Flexible;
                inventoryManager.ClearEquippedSlots();
            }
        }
        //clear boolean to undo selection when slots destroyed.
        tnksel.SetSelected(false, null);
    }
}
