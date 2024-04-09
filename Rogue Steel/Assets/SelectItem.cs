using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectItem : MonoBehaviour, IPointerClickHandler
{
    public bool selected;
    public bool filled;
    //Parent interface
    public GameObject ParInt;
    public ItemSelected ParIntScript;
    public string type;
    public InventoryCellSlot inv;
    public TankCellSlot tank;
    public GameObject Inventory;
    // Start is called before the first frame update
    void Start()
    {
        ParInt = this.transform.parent.gameObject;
        ParIntScript = ParInt.GetComponent<ItemSelected>();
        type = ParIntScript.type;
        selected = false;
        filled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Generic Cell Script");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    //choose slot
    public void OnLeftClick()
    {
        //bool toggle
        selected = !selected;
        switch (selected)
        {
            case false:
                ParIntScript.SetSelected(false,null);
                //ParIntScript.selected = false;
                //ParIntScript.selectedGameObject = null;
                break;
            case true:
                ParIntScript.SetSelected(true, transform.gameObject);
                //ParIntScript.selected = true;
                //ParIntScript.selectedGameObject = transform.gameObject;
                break;
        }
    }
    //clear slot
    public void OnRightClick()
    {
        selected = false;
        ParIntScript.SetSelected(false, null);
        if (tank && filled)
        {
            Inventory.GetComponent<InventoryHandling>().AddToInventory(tank.moniker,tank.type);
        }
        //ParIntScript.selected = false;
        //ParIntScript.selectedGameObject = null;
    }
    //refund item
    private void OnDestroy()
    {
        if (tank && filled)
        {
            Inventory.GetComponent<InventoryHandling>().AddToInventory(tank.moniker, tank.type);
        }
    }
}
