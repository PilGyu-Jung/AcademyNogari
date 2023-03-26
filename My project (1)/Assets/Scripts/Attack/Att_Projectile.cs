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
                bb.bulletmove(pType);
                //list_bullet.Add(Instantiate(projectile, shotPos.position, transform.rotation));
                //foreach (var item in list_bullet)
                //{
                //    item.transform.parent = gameObject.transform;

                //}
                //for (int i = 0; i < list_bullet.Count; i++)
                //{
                //    list_bullet[i].transform.parent = gameObject.transform;
                //}
                //foreach (var items in list_bullet)
                //{
                //    list_proj_bullet.Add(items.transform.GetComponent<Bullet>());
                //}
                //for (int i = 0; i < list_bullet.Count; i++)
                //{
                //    list_proj_bullet[i] = list_bullet[i].GetComponent<Bullet>();
                //}
                
                break;

            case ProjectileType.NONLINE:
                //bullet = Instantiate(projectile, shotPos.position, transform.rotation);
                //bullet.transform.parent = gameObject.transform;
                //proj_bullet = bullet.GetComponent<Bullet>();
                //proj_bullet.bulletmove(pType);


                break;
            case ProjectileType.HOWITZER:

                break;
        }


    }
}
