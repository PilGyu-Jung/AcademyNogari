using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    public Transform parentAfterDrag;
    public Transform parentBeforeDrag;

    [SerializeField]
    Transform root_ShopSlot;
    [SerializeField]
    List<Transform> buttons_shopslot;

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

        root_ShopSlot = ItemShop.Instance.transform.GetChild(1).GetChild(0).transform;
        for (int i = 0; i < root_ShopSlot.childCount; i++)
        {
            buttons_shopslot.Add(root_ShopSlot.GetChild(i).transform);
        }
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

        for (int i = 0; i < buttons_shopslot.Count; i++)
        {/* 드래그를 시작하면 상점창에 있는 Button의 interactable, Image의 raycastTarget을 잠시 끈다.*/
            buttons_shopslot[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
            buttons_shopslot[i].GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (isStore)
            return;

        transform.position = Input.mousePosition;
        for (int i = 0; i < buttons_shopslot.Count; i++)
        {/* 드래그를 하는중에 상점창에 있는 Button의 interactable, Image의 raycastTarget을 잠시 끈다.*/
            buttons_shopslot[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
            buttons_shopslot[i].GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
        }

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (isStore)
            return;

        dragging = false;
        transform.SetParent(parentAfterDrag);
        image_item.raycastTarget = true;

        for (int i = 0; i < buttons_shopslot.Count; i++)
        {/* 드래그가 끝나면 상점창에 있는 '자식이 있는 Slot들' 을 골라 Button의 interactable, Image의 raycastTarget을 다시 true.*/
            if (buttons_shopslot[i].childCount > 0)
            {
                buttons_shopslot[i].GetComponent<UnityEngine.UI.Button>().interactable = true;
                buttons_shopslot[i].GetComponent<UnityEngine.UI.Image>().raycastTarget = true;
            }
            else
            {

                buttons_shopslot[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
                buttons_shopslot[i].GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
            }
        }

    }

}
