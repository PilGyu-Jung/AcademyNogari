using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    public Entity   attackUser;
    public Entity   attackTarget;
    public Vector3  attackPoint;
    public float    coolTime;
    public float    dmg_max;
    public float    dmg_min;
    public enum Type_Att { MELEE,HITSCAN,LINEAR_PROJECTILE,NONLINE_PROJECTILE,RANGE,RANDOMPOS,HOWITZER};
    //����Ÿ��: �и�, ��Ʈ��ĵ, ��������ü,���� ����ü, ����, ������ ����, �����.
    public Type_Att attkType;

}
