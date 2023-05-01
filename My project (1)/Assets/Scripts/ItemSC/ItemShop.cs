using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemShop : MonoBehaviour,IDropHandler
{
    private static ItemShop instance = null;

    public GameObject itemContainer;
    public List<Transform> List_shopChild;
    public Transform shopSlot_root;
    public Transform Inventory_root;

    public static ItemShop Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public System.Action<Items> OnClick_ShopSlot;

    private void Awake()
    {
        for (int i = 0; i < shopSlot_root.childCount; i++)
        {
            List_shopChild.Add(shopSlot_root.GetChild(i).GetComponent<Transform>());
        }

        DisplayItems();

    }
    // Start is called before the first frame update
    void Start()
    {

        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < shopSlot_root.childCount; i++)
        {
            if(shopSlot_root.GetChild(i).transform.childCount > 0)
            {
                shopSlot_root.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            else
            {
                shopSlot_root.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = false;

            }
        }

    }


    void DisplayItems()
    {
        for (int i = 0; i < ItemManager.Instance.ItemList.Count; i++)
        {            
            List_shopChild[i].GetComponent<DroppableSlot>().SetItemInSlot(ItemManager.Instance.ItemList[i]);
        }
    }

    public void OnClickShopSlot(DroppableSlot slot)
    {
        //OnClick_ShopSlot(slot.curItem.contain_item);
        //Debug.Log(slot.curItem.contain_item.itemName);

        if (OnClick_ShopSlot != null)
        {
            OnClick_ShopSlot(slot.curItem.contain_item);
        }
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        //Debug.Log();
        GameObject dropped = eventData.pointerDrag;
        Debug.Log(dropped.GetComponent<DraggableItem>().contain_item.itemName);
        Destroy(dropped);
        // selling 함수 추가.



        for (int i = 0; i < shopSlot_root.childCount; i++)
        {/* 드래그가 끝나면 상점창에 있는 '자식이 있는 Slot들' 을 골라 Button의 interactable, Image의 raycastTarget을 다시 true.*/
            if (shopSlot_root.GetChild(i).transform.childCount > 0)
            {
                shopSlot_root.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = true;
                shopSlot_root.GetChild(i).GetComponent<UnityEngine.UI.Image>().raycastTarget = true;

            }
            else
            {
                shopSlot_root.GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = false;
                shopSlot_root.GetChild(i).GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
            }
        }

    }

}
