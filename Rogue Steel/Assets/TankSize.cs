using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TankSize : MonoBehaviour, IPointerClickHandler
{
    public Vector2 size;
    public GameObject tnkpnl;
    public GridLayoutGroup tnkSize;
    public GameObject cell;
    private void Start()
    {
        tnkpnl = GameObject.Find("Tank Panel");
        tnkSize = tnkpnl.GetComponent<GridLayoutGroup>();
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
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    GameObject temp = Instantiate(cell, tnkpnl.transform);
                    temp.AddComponent<TankCellSlot>().pos = new Vector2(x,y);
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
            }
        }
    }
}
