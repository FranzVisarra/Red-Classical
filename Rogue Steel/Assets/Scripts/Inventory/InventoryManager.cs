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
    public EquippedSlot[] equippedSlot;
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
        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisItemSelected = false;
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