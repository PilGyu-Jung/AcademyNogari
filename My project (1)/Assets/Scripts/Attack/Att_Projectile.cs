using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Att_Projectile : AttackType
{
    public GameObject projectile;
    public Transform shotPos;
    public enum ProjectileType { LINEAR, NONLINE, HOWITZER }
    public ProjectileType pType;
    public Transform targetPos;

    public float bulletForce;
    public float lifeTime;
    [Header("NON-LINE")]
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        attackTarget = FindObjectOfType<PlayerController>();
        targetPos = attackTarget.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            shootBullet();
        }
    }

    void shootBullet()
    {
        switch (pType)
        {
            case ProjectileType.LINEAR:
                GameObject bullet = Instantiate(projectile,shotPos.position,transform.rotation);
                bullet.transform.position = shotPos.transform.position;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
                
                Destroy(bullet,lifeTime);
                break;
            case ProjectileType.NONLINE:
                bullet = Instantiate(projectile, shotPos.position, transform.rotation);

                Vector3 direction = (Vector3)targetPos.position - bullet.transform.position;
                direction.Normalize();
                Vector3 rotateAmount = Vector3.Cross(direction, bullet.transform.forward);
                bullet.GetComponent<Rigidbody>().angularVelocity = -rotateAmount * rotateSpeed;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);


                break;
            case ProjectileType.HOWITZER:

                break;
        }


    }
}
