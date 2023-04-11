using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Team { A,B};
    public enum State { IDLE, DETECT, CHASE, ATTACK, DEAD };

    public Weapon hitWeapon;
    public Entity hitEntity;
    public Renderer baseRenderer;


    GameObject findParent;
    RandomLootingObject randomLtObj;

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

    private void Awake()
    {
        baseRenderer = GetComponent<Renderer>();
    }

    public void ApplyDamage(Entity _target,float dmg)
    {
        if(!_target.isdead)
            _target.hp -= dmg;
        Debug.Log(this.gameObject.name + "is attacking to " + _target.name + this.damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Weapon")
        {
            hitWeapon = other.gameObject.GetComponent<Weapon>();
            //FindEntity_recur(ref findParent,hitEntity);
            hitEntity = hitWeapon.user;

            OnHit(hitWeapon,hitEntity);
            //OnHit_proto(hitWeapon);
        }
    }
    void FindEntity_recur(ref GameObject _target,Entity _targetEntity)
    { // _targetEntity가 null이 아닐때까지 부모오브젝트 컴포넌트 검색.
        _targetEntity = _target.gameObject.GetComponentInParent<Entity>();
        if (_targetEntity == null)
        {

            _target = _target.transform.parent.gameObject;
            FindEntity_recur(ref _target, _targetEntity);
        }

        else
            return;
    }

    public void OnHit(Weapon h_weapon,Entity h_entity)
    {
        if(h_weapon!= null && h_entity!= null)
        {// weapon 과 Entity가 null이 아닐시에 damage가 합산해서 적용된다.
            this.hp -= h_weapon.weapon_damage + h_entity.damage;
        }
        else if(h_weapon ==null && h_entity!= null)
        {// Entity가 무기를 들고있지 않은 경우에는 entity damage만 적용된다.
            this.hp -= h_entity.damage;
        }
        else
        {
            this.hp -= h_weapon.weapon_damage;
        }
    }

    public void OnHit_proto(Weapon h_weapon)
    {
        if (h_weapon != null)
            this.hp -= h_weapon.weapon_damage;
    }

    virtual public void OnDead(bool d)
    {

    }
}
