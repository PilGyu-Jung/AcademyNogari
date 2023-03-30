using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Att_Projectile : AttackType
{
    public GameObject projectile;
    //public List<GameObject> list_bullet;
    public Transform shotPos;
    public enum ProjectileType { LINEAR, NONLINE, HOWITZER }
    public ProjectileType pType;
    public Transform targetPos;
    //public List<Bullet> list_proj_bullet;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            shootBullet();
        }
    }

    void shootBullet()
    {
        switch (pType)
        {
            case ProjectileType.LINEAR:
                GameObject bullet = Instantiate(projectile, shotPos.position, transform.rotation);
                bullet.transform.parent = transform;
                Bullet bb = bullet.transform.GetComponent<Bullet>();
                bb.isnonLine = false;
                //bb.bulletmove(pType);
                bb.GetComponent<Rigidbody>().AddForce(bb.transform.forward * bb.bulletForce, ForceMode.Impulse);
                
                break;

            case ProjectileType.NONLINE:
                GameObject bullet2 = Instantiate(projectile, shotPos.position, transform.rotation);
                bullet2.transform.parent = transform;
                Bullet bb2 = bullet2.transform.GetComponent<Bullet>();
                bb2.isnonLine = true;



                break;
            case ProjectileType.HOWITZER:
                GameObject bullet3 = Instantiate(projectile, shotPos.position, transform.rotation);
                bullet3.transform.parent = transform;
                Bullet bb3 = bullet3.transform.GetComponent<Bullet>();
                bb3.ishowitzer = true;

                break;
        }


    }
}
