using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Team { A,B};
    public enum State { IDLE, DETECT, CHASE, ATTACK, DEAD };

    public float hp;
    public float mp;

    public float speed_Walk;
    public float speed_Run;

    public float damage;
    public float attackBetTime;

    public bool isdead;
    public bool isattack;
    public bool isrun;

    public void ApplyDamage(Entity _target)
    {
        _target.hp -= damage;
        Debug.Log(this.gameObject.name + "is attacking to " + _target.name + this.damage);
    }

    public void DropGold()
    {

    }
}
