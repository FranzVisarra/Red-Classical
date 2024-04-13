using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    //============ ITEM DATA ===========//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    //============ ITEM SLOT ==========//

    [SerializeField]
    private Image itemImage;

    //=========== EQUIPPED SLOT =======//
    [SerializeField]
    //private EquippedSlot hullSlot, baseSlot, heavyWeaponSlot, lightWeaponSlot, tracksSlot, crewSlot, lightAmmoSlot, heavyAmmoSlot;


    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        //New Part
        ParInt = this.transform.parent.gameObject;
        ParIntScript = ParInt.GetComponent<ItemSelected>();
        selected = false;
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        //Check if slot is full 
        if (isFull)
            return quantity;
        //update item type
        this.itemType = itemType;
        //update name, quantity, image and description
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;
        this.quantity = 1;

        isFull = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //New Part
            OnRightClick();
        }
    }
    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            EquipGear();
        }
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
    }
    private void EmptySlot()
    {
        itemImage.sprite = emptySprite;
        isFull = false;
        itemName = null;
        quantity = 0;

    }

    private void EquipGear()
    {
        // Find an available equipped slot
        List<GameObject> equippedSlots = inventoryManager.EquippedSlots;
        foreach (var equippedSlotGameObject in equippedSlots)
        {
            // Get the EquippedSlot component from the GameObject
            EquippedSlot equippedSlot = equippedSlotGameObject.GetComponent<EquippedSlot>();

            // Check if the slot is empty
            if (!equippedSlot.slotInUse)
            {
                // Equip the item to the slot
                equippedSlot.EquipGear(itemSprite, itemName, itemDescription);
                // Clear the equipment slot
                EmptySlot();
                return; // Exit the loop after equipping the item
            }
        }


        // If no available slot was found, handle this case (e.g., show a message).
        Debug.Log("No available equipped slots found.");
    }

    //New Stuff
    public bool selected;
    //Parent interface
    public GameObject ParInt;
    public ItemSelected ParIntScript;
    public void OnRightClick()
    {
        //bool toggle
        selected = !selected;
        ParIntScript.selected = true;
        ParIntScript.selectedGameObject = transform.gameObject;
    }
}
