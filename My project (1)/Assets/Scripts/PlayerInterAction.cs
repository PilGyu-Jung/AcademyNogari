using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
    public int amoutHaveCoin;
    public float radius_absorb;
    public SphereCollider pickItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {


            Destroy(other.gameObject);
        }
    }

    void PickCoin(Coin coin)
    {

    }
}
