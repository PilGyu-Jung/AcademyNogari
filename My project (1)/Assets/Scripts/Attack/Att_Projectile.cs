using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Att_Projectile : AttackType
{
    public GameObject projectile;
    public Transform shotPos;
    public enum ProjectileType { LINEAR, NONLINE, HOWITZER }
    public ProjectileType pType;

    public float bulletForce;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {

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
                GameObject bullet = Instantiate(projectile);
                bullet.transform.position = shotPos.transform.position;
                bullet.GetComponent<Rigidbody>().AddForce(new Vector3(bulletForce, 0, 0), ForceMode.Force);
                
                Destroy(bullet,lifeTime);
                break;
            case ProjectileType.NONLINE:

                break;
            case ProjectileType.HOWITZER:

                break;
        }


    }
}
