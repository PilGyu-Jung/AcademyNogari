using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Team { A,B};
    public enum State { IDLE, DETECT, CHASE, ATTACK, DEAD };

    //public float maxHP;
    //public float hp { 
    //    get 
    //    { 
    //        return maxHP; 
    //    } 
    //    set 
    //    { 
    //        if (hp > maxHP)
    //        {
    //            hp = maxHP;
    //            isdead = false;
    //        }
    //        else if (hp <= 0)
    //        {
    //            isdead = true;
    //            hp = 0;
    //        }
    //        else
    //            isdead = false;
    //    } 
    //}
    public float hp;
    public float mp;

    public float speed_Walk;
    public float speed_Run;

    public float damage;
    public float attackBetTime;

    public bool isdead;
    public bool isattack;
    public bool isrun;

    public void ApplyDamage(Entity _target,float dmg)
    {
        if(!_target.isdead)
            _target.hp -= dmg;
        Debug.Log(this.gameObject.name + "is attacking to " + _target.name + this.damage);
    }

    public void DropGold()
    {

    }
}
