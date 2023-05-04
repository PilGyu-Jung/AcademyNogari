using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroAttackType
{
    MELEE, HITSCAN, PROJECTILE
}


[System.Serializable]
public class Hero 
{
    public string name_Hero;
    public Sprite image_Hero;
    [TextArea]
    public string desc_Hero;
    public HeroAttackType type_HA;

}
