using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform root_inventory;
    public ItemShop shop;

    [SerializeField]
    List<DroppableSlot> slots; 
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < root_inventory.childCount; i++)
        {
            slots.Add(root_inventory.GetChild(i).GetComponent<DroppableSlot>());
        }
        shop.OnClick_ShopSlot += BuyingItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuyingItem(Items item)
    {
        //Debug.Log(item.itemName);
        var emptySlot = slots.Find(t =>
        {
            return /*t.transform.childCount == 0 ||*/ 
            t.curItem == null;
        });

        if(emptySlot != null)
        {
            emptySlot.GetComponent<DroppableSlot>().SetItemInSlot(item);
        }
    }
}
