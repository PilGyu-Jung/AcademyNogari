using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public PlayerController pController;
    public BoxCollider attackbox;
    public LayerMask colMask;

    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == colMask)
        {
            target = other.gameObject.GetComponent<Entity>();
            Debug.Log("MeleeWeaponCollider! :" + target.gameObject.name);

            if (target != null)
            {
                pController.ApplyDamage(target,pController.damage);

            }
        }
    }
}
