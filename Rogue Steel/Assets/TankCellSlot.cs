using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankCellSlot : MonoBehaviour, IPointerClickHandler
{
    public Vector2 pos;
    public string type;
    public string moniker;
    public SelectItem item;
    void Start()
    {
        item = gameObject.GetComponent<SelectItem>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Tank Cell Slot");
    }
    public void SetValues(string type, string moniker)
    {
        this.type = type;
        this.moniker = moniker;
    }
}
