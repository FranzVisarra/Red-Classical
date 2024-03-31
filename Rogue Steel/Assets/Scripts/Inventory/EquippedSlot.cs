using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler

{
    //SLOT APPEARANCE//
    [SerializeField]
    private UnityEngine.UI.Image slotImage;
    [SerializeField]
    private TMP_Text slotName;
    [SerializeField]
    private UnityEngine.UI.Image playerDisplayImage;
    [SerializeField]
    private UnityEngine.UI.Image playerDisplayImage2;

    //SLOT DATA//
    [SerializeField]
    private ItemType itemType = new ItemType();
    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;
    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
    }

    //OTHER VARIABLES//
    private bool slotInUse;

    [SerializeField]
    public GameObject selectedShader;
    [SerializeField]
    public bool thisItemSelected;
    [SerializeField]
    private Sprite emptySprite;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    void OnLeftClick()
    {
        if (thisItemSelected && slotInUse)
            UnEquipGear();

        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }
    void OnRightClick()
    {
        UnEquipGear();
    }


    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        if (slotInUse)
        {
            UnEquipGear();
        }
        //Update Image
        playerDisplayImage.enabled = true;
        slotImage.enabled = true;
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        //Update Data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        //Update Display Image
        playerDisplayImage.sprite = itemSprite;
        //playerDisplayImage2.sprite = itemSprite;

        //Update Player Stats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].EquipItem();
            }
        }

        slotInUse = true;

    }

    public void UnEquipGear()
    {
        inventoryManager.DeselectAllSlots();
        inventoryManager.AddItem(itemName, 1, itemSprite, itemDescription, itemType);
        //Update Slot Image
        this.itemSprite = emptySprite;
        slotImage.enabled = false;
        slotName.enabled = true;

        playerDisplayImage.enabled = false;
        //playerDisplayImage2.sprite = emptySprite;

        //Update Player Stats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].UnEquipItem();
            }
        }
    }

}