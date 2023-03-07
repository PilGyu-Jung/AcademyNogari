using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public PlayerController pController;
    public LayerMask colMask;
    public Entity target;

    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == colMask)
        {
            target = other.gameObject.GetComponent<Entity>();
            if(target != null)
            {
                pController.ApplyDamage(target,pController.damage);
                Debug.Log("asdklnmaskdnaskd"+target.gameObject.name);

            }
        }
    }
}
