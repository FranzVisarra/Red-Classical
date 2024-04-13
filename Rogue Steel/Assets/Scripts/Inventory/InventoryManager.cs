using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;
    public List<GameObject> EquippedSlots = new List<GameObject>();

    public Sprite selectedItemSprite;
    public string selectedItemName;
    public string selectedItemDescription;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory();
        }
        if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            Equipment();
        }
    }

    public void AddEquippedSlot(GameObject slot)
    {
        EquippedSlots.Add(slot);
    }
    public void ClearEquippedSlots()
    {
        EquippedSlots.Clear();
    }

    void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            //Time.timeScale = 1;
        }
        else
        {
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
            //Time.timeScale = 0;
        }
    }
    void Equipment()
    {
        if (EquipmentMenu.activeSelf)
        {
            EquipmentMenu.SetActive(false);
            InventoryMenu.SetActive(false);
            //Time.timeScale = 1;
        }
        else
        {
            EquipmentMenu.SetActive(true);
            InventoryMenu.SetActive(false);
            //Time.timeScale = 0;
        }
    }
    public void ReceiveEquipmentSlotInfo(Sprite itemSprite, string itemName, string itemDescription)
    {
        selectedItemSprite = itemSprite;
        selectedItemName = itemName;
        selectedItemDescription = itemDescription;
        Debug.Log("Inventory recieved " + selectedItemName);
    }

    public string SendEquipmentSlotInfo()
    {
        return selectedItemName;

    }



    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (itemType == ItemType.currency || itemType == ItemType.collectible)
        {
            Debug.Log("Item Name: " + itemName + " | Quantity: " + quantity + "| Sprite: " + itemSprite);
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    return leftOverItems;

                }
            }
            return quantity;
        }
        else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].isFull == false && equipmentSlot[i].itemName == itemName || equipmentSlot[i].quantity == 0)
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    return leftOverItems;

                }
            }
            return quantity;
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].selectedShader.SetActive(false);
            equipmentSlot[i].thisItemSelected = false;
        }
        foreach (GameObject equippedSlotGameObject in EquippedSlots)
        {
            EquippedSlot equippedSlot = equippedSlotGameObject.GetComponent<EquippedSlot>();
            if (equippedSlot != null)
            {
                equippedSlot.selectedShader.SetActive(false);
                equippedSlot.thisItemSelected = false;
            }
        }

    }
    public void ClearSlot()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            if (equipmentSlot[i].thisItemSelected == true)
            {
                equipmentSlot[i].itemImage.sprite = equipmentSlot[i].emptySprite;
                equipmentSlot[i].isFull = false;
                equipmentSlot[i].itemName = null;
                equipmentSlot[i].quantity = 0;
            }
        }
    }
}



public enum ItemType
{
    equipment,
    currency,
    collectible,
    hull,
    tankBase,
    heavyWeapon,
    lightWeapon,
    tracks,
    crew,
    lightAmmo,
    heavyAmmo
};
