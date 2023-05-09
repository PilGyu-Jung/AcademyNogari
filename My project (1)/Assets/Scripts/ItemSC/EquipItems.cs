using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipItems : Items
{
    public enum Equiptype { HELMET, ARMOR, AMULET1, AMULET2, WEAPON1, WEAPON2, GLOVES, BOOTS };
    public Equiptype eType;
    
    public EquipItems()
    {
        this.isEquipItem = true;
    }
}
