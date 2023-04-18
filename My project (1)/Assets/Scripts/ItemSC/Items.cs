using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    [Header("--------<Shop_Information>---------")]
    public string       itemName;
    public Sprite       itemImage;
    [TextArea]
    public string       itemDesc;
    public int          itemPrice;
    [Header("--------<Inner_Information>---------")]
    public string       itemCode;
    public GameObject   itemObject;
    public bool         stackable;
    public int          durability;
}
