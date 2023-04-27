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
    public DraggableItem curItem;

    bool enterSlot = false;
    public bool haveItem = false;

    private void Start()
    {
        if(isSlot_store)
        {
            if(transform.childCount > 0 )
            {
                transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
                transform.GetChild(0).GetComponent<DraggableItem>().isStore = true;
            }
            else
            {
                return;
            }
        }
    }
    private void Update()
    {
        if(transform.childCount > 0)
            curItem = transform.GetChild(0).GetComponent<DraggableItem>();
        if(transform.childCount == 0)
        {
            curItem = null;
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

    //public void SwapItems(DraggableItem itemA,DraggableItem itemB,DroppableSlot slotA,DroppableSlot slotB)
    //{
    //    DraggableItem temp = itemA;
    //    itemA = itemB;
    //    itemB = temp;

    //    slotA.curItem = itemA;
    //    slotB.curItem = itemB;
    //    slotA.ArrangeItemToSlot(itemA);
    //    slotB.ArrangeItemToSlot(itemB);

    //}

    public void SwapItems(Transform slotA, Transform slotB)
    {
        Items temp = slotA.GetComponent<DroppableSlot>().getItem;
        slotA.GetComponent<DroppableSlot>().getItem
            = slotB.GetComponent<DroppableSlot>().getItem;
        slotB.GetComponent<DroppableSlot>().getItem = temp;

        slotA.GetComponent<DroppableSlot>().ArrangeItemToSlot(slotA.GetChild(0).GetComponent<DraggableItem>());
        ArrangeItemToSlot(slotB.GetChild(0).GetComponent<DraggableItem>());
    }

    public void ArrangeItemToSlot(DraggableItem dragItem)
    {
        dragItem.contain_item = getItem;
    }

    public void ArrangeItemToSlot(Items item)
    {
        transform.GetChild(0).GetComponent<DraggableItem>().contain_item = item;
        item = getItem;
    }

    public void RemoveItemInSlot(DroppableSlot targetSlot)
    {
        targetSlot.getItem.clearItems();
        haveItem = false;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (isSlot_store)
            return;

        if(!isSlot_equip)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            if (transform.childCount == 0)
            {
                if (draggableItem != null)
                {
                    getItem = draggableItem.contain_item;
                    draggableItem.parentAfterDrag = transform;
                    ArrangeItemToSlot(draggableItem);
                }
                else
                    return;
            }
            else
            {
                if(draggableItem != null)
                {
                    SwapItems(draggableItem.parentBeforeDrag, draggableItem.parentAfterDrag);
                }
            }
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

        if (enterSlot && transform.childCount == 1)
        {
            if (curItem.dragging == true)
            {
                TabManager.Instance.popup.gameObject.SetActive(false);
            }

            else
            {
                TabManager.Instance.popup.gameObject.SetActive(true);

                //TabManager.Instance.text_name.text = getItem.itemName;
                //TabManager.Instance.text_expl.text = getItem.itemDesc;
                //TabManager.Instance.text_price.text = getItem.itemPrice.ToString();

                TabManager.Instance.text_name.text = curItem.contain_item.itemName;
                TabManager.Instance.text_expl.text = curItem.contain_item.itemDesc;
                TabManager.Instance.text_price.text = curItem.contain_item.itemPrice.ToString();

                if ((Screen.height / 2) < Mathf.Abs(TabManager.Instance.popup.rect.height - TabManager.Instance.popup.anchoredPosition.y))
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
        else if(enterSlot && transform.childCount > 1)
        {

        }
        else
        {
            TabManager.Instance.popup.gameObject.SetActive(false);
        }
    }
}
