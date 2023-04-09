using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableSlot : MonoBehaviour,IDropHandler
{
    bool isequipSlot;
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

}
