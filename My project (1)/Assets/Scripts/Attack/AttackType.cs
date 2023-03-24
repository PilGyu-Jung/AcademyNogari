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
    //공격타입: 밀리, 히트스캔, 선형투사체,비선형 투사체, 범위, 랜덤한 지역, 곡사포.
    public Type_Att attkType;

}
