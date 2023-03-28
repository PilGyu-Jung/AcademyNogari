using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime_bullet;
    public float rotateSpeed;
    public float bulletForce;
    public float howitz_height;

    public bool isnonLine;

    Rigidbody rb;
    //Att_Projectile att_prj;
    Transform targetTransform;
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 middlePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //att_prj = transform.parent.GetComponent<Att_Projectile>();
        targetTransform = FindObjectOfType<PlayerController>().transform;
        Destroy(gameObject, lifetime_bullet);
        startPosition = transform.position;
        endPosition = targetTransform.position;
        middlePosition 
            = new Vector3((startPosition.x + endPosition.x) / 2, howitz_height, (startPosition.z + endPosition.z) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        nonLine_bulletmove(isnonLine);
    }

    public void nonLine_bulletmove(bool nonLine)
    {
        if (!nonLine)
            return;

        transform.LookAt(targetTransform);
        rb.velocity = (targetTransform.position - transform.position).normalized * bulletForce;

    }

    void howitzer_bulletmove(bool howitzer)
    {
        if (!howitzer)
            return;

    }
}
