using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime_bullet;
    public float rotateSpeed;
    public float bulletForce;

    Rigidbody rb;
    //Att_Projectile att_prj;
    Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //att_prj = transform.parent.GetComponent<Att_Projectile>();

        Destroy(gameObject, lifetime_bullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bulletmove(Att_Projectile.ProjectileType type_proj)
    {
        switch (type_proj)
        {
            case Att_Projectile.ProjectileType.LINEAR:
                rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);

                break;

            case Att_Projectile.ProjectileType.NONLINE:
                Vector3 direction = (Vector3)targetTransform.position - transform.position;
                direction.Normalize();
                Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);
                rb.angularVelocity = -rotateAmount * rotateSpeed;
                rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);

                break;
            case Att_Projectile.ProjectileType.HOWITZER:

                break;
        }

    }
}
