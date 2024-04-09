using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectItem : MonoBehaviour
{
    public bool selected;
    //Parent interface
    public GameObject ParInt;
    public ItemSelected ParIntScript;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        ParInt = this.transform.parent.gameObject;
        ParIntScript = ParInt.GetComponent<ItemSelected>();
        type = ParIntScript.type;
        selected = false;
    }
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
    //choose slot
    public void OnLeftClick()
    {
        //bool toggle
        selected = !selected;
        switch (selected)
        {
            case false:
                ParIntScript.selected = false;
                ParIntScript.selectedGameObject = null;
                break;
            case true:
                ParIntScript.selected = true;
                ParIntScript.selectedGameObject = transform.gameObject;
                break;
        }
    }
    //clear slot
    public void OnRightClick()
    {
        selected = false;
        ParIntScript.selectedGameObject = null;
    }
}
