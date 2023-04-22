using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    public Transform parentAfterDrag;
    public Transform parentBeforeDrag;
    public Image image_item;
    public Items contain_item;
    Color imageColor;

    public bool dragging;
    public bool isStore;

    private void Start()
    {
        image_item = GetComponent<Image>();
        //image_item.color = imageColor;
        imageColor = image_item.color;
    }
    private void Update()
    {
        image_item.sprite = contain_item.itemImage;

        if (contain_item != null)
        {
            imageColor.r = 1f;
            imageColor.g = 1f;
            imageColor.b = 1f;
            imageColor.a = 1f;
        }
        else
        {
            imageColor.a = 0f;

        }
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (isStore)
            return;

        dragging = true;
        parentAfterDrag = transform.parent;
        parentBeforeDrag = parentAfterDrag;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image_item.raycastTarget = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (isStore)
            return;

        transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isStore)
            return;

        dragging = false;
        transform.SetParent(parentAfterDrag);
        image_item.raycastTarget = true;
    }

}
