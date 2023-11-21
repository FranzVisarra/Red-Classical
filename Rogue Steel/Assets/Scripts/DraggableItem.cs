using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;  // Declare the Image variable

    private Transform parentAfterDrag;
    private Canvas canvas;
    public RectTransform rectTransform;

    // Property to get or set parentAfterDrag
    public Transform ParentAfterDrag
    {
        get { return parentAfterDrag; }
        set { parentAfterDrag = value; }
    }

    private void Start()
    {
        // Assuming the Canvas is on the same GameObject as this script.
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();  // Initialize the Image variable
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert the mouse position to a position in the Canvas space.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out Vector2 localPos);

        // Set the position of the RectTransform to the calculated local position.
        rectTransform.localPosition = localPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
