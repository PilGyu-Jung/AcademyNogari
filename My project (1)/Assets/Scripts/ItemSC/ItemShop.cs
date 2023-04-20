using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public GameObject itemContainer;
    public List<GameObject> List_ShopSlot; 

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in GetComponentsInChildren<DroppableSlot>())
        {
            List_ShopSlot.Add(item.gameObject);
        }
        DisplayItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayItems()
    {
        for (int i = 0; i < ItemManager.Instance.ItemList.Count; i++)
        {
            List_ShopSlot[i].GetComponent<DroppableSlot>().SetItemInSlot(ItemManager.Instance.ItemList[i]);
        }
    }
}
