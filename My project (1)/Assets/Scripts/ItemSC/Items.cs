using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    [Header("--------<Shop_Description>---------")]
    public string       itemName;
    public Sprite       itemImage;
    [TextArea]
    public string       itemDesc;
    public int          itemPrice;
    [Header("--------<Inner_Information>---------")]
    public string       itemCode;
    public GameObject   itemObject;
    public bool         stackable;
    public bool         isEquipItem;
    public int          durability;

    public void clearItems()
    {
        itemName = "";
        itemImage = null;
        itemDesc = "";
        itemPrice = 0;
        itemCode = "";
        itemObject = null;
        stackable = false;
        durability = 0;
    }

    public bool EquipmentItem
    {
        get { return isEquipItem; }
        set
        {
            isEquipItem = value;
        }
    }
}
