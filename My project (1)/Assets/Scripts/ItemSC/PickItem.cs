using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : PickObject
{
    public enum ItemType { CONSUME, BUFF, EQUIP }
    public ItemType iType;

}
