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
    private EquippedSlot hullSlot, baseSlot, heavyWeaponSlot, lightWeaponSlot, tracksSlot, crewSlot, lightAmmoSlot, heavyAmmoSlot;


    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
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
            //OnRightClick();
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
        if (itemType == ItemType.hull)
            hullSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.tankBase)
            baseSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.heavyWeapon)
            heavyWeaponSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.lightWeapon)
            lightWeaponSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.tracks)
            tracksSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.crew)
            crewSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.heavyAmmo)
            heavyAmmoSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.lightAmmo)
            lightAmmoSlot.EquipGear(itemSprite, itemName, itemDescription);
        EmptySlot();
    }
}
