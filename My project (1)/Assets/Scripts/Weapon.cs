using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float weapon_damage;
    public enum WeaponType { Melee,Projectile,Hitscan}
    public WeaponType wType;

    public Entity target;
    public Entity user;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
