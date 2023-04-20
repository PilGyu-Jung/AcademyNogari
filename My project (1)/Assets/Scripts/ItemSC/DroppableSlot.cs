using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableSlot : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
{

    public bool isSlot_equip;
    public bool isSlot_store;
    public DraggableItem itemContainer;

    bool enterSlot = false;

    public void SetItemInSlot(Items item)
    {
        //var itemobj= Instantiate(item, this.transform.position, Quaternion.identity);
        //itemobj.transform.SetParent(this.gameObject.transform);
        var item_container = Instantiate(itemContainer, this.transform.position, Quaternion.identity);

        itemContainer.contain_item = item;
        item_container.transform.SetParent(this.transform);

    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;

        }
        else
        {
            
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(isSlot_store)
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
        if(enterSlot)
        {
            TabManager.Instance.popup.gameObject.SetActive(true);
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
    }
}
