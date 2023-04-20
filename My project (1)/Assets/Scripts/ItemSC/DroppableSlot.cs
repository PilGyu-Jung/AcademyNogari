using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableSlot : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
{

    public bool isSlot_equip;
    public bool isSlot_store;
    public DraggableItem itemContainer;
    public Items getItem;

    bool enterSlot = false;
    public bool haveItem = false;

    private void Update()
    {
        if(transform.childCount == 0)
        {
            getItem = null;
            haveItem = false;
        }
    }

    public void SetItemInSlot(Items item)
    {
        //var itemobj= Instantiate(item, this.transform.position, Quaternion.identity);
        //itemobj.transform.SetParent(this.gameObject.transform);
        haveItem = true;
        itemContainer.contain_item = item;
        var item_container = Instantiate(itemContainer, this.transform.position, Quaternion.identity);

        getItem = item;
        item_container.transform.SetParent(this.transform);
    }

    public void ArrangeItemToSlot(DraggableItem dragItem)
    {
        dragItem.contain_item = getItem;
    }

    public void RemoveItemInSlot(DroppableSlot targetSlot)
    {
        targetSlot.getItem.clearItems();
        haveItem = false;
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            getItem = draggableItem.contain_item;

            draggableItem.parentAfterDrag = transform;
            //draggableItem.parentAfterDrag.GetComponent<DroppableSlot>().SetItemInSlot(draggableItem.contain_item);
            //draggableItem.parentBeforeDrag.GetComponent<DroppableSlot>().removeItemInSlot();
            ArrangeItemToSlot(draggableItem);
            //RemoveItemInSlot(draggableItem.parentBeforeDrag.GetComponent<DroppableSlot>());
        }
        else
        {
            
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(isSlot_store && transform.childCount >= 1)
        {
            enterSlot = true;
            TabManager.Instance.popup.gameObject.SetActive(true);
            TabManager.Instance.popup.SetAsLastSibling();
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (isSlot_store)
        {
            enterSlot = false;
            TabManager.Instance.popup.gameObject.SetActive(false);
        }
    }

    void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
    {
        if(enterSlot && transform.childCount > 0)
        {
            TabManager.Instance.popup.gameObject.SetActive(true);

            TabManager.Instance.text_name.text = getItem.itemName;
            TabManager.Instance.text_expl.text = getItem.itemDesc;
            TabManager.Instance.text_price.text = getItem.itemPrice.ToString();

            if ((Screen.height / 2) < Mathf.Abs(TabManager.Instance.popup.rect.height- TabManager.Instance.popup.anchoredPosition.y))
            {
                TabManager.Instance.popup.pivot = new Vector2(0f, 0f);
                TabManager.Instance.popup.position = eventData.position;

            }
            else
            {
                TabManager.Instance.popup.pivot = new Vector2(0f, 1f);
                TabManager.Instance.popup.position = eventData.position;

            }
        }
        else
        {
            TabManager.Instance.popup.gameObject.SetActive(false);
        }
    }
}
