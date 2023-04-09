using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Assets/Item")]
public class G_Item : ScriptableObject
{
    public Sprite itemImage;
    public enum G_ItemType {CONSUME,WEAPON,EQUIP };
    public G_ItemType itemtype;
    public string itemName;
    public GameObject itemObject;
    [TextArea]
    public string itemDescription;
    public string itemID;
    public int itemCost;
    public int itemSell;
}
