using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
    public int amoutHaveCoin;
    public float radius_absorb;
    public SphereCollider pickItem;
    public PickObject pickObj;
    public int getCoin;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            pickObj = other.gameObject.GetComponent<PickObject>();
            PickCoin(pickObj);
            Destroy(other.gameObject);
        }
        //else if(other.gameObject.layer == LayerMask)
        Debug.Log(other.gameObject.name);
    }
    void PickCoin(PickObject pObj)
    {
        switch (pObj.cType)
        {
            case PickObject.CoinType.C10:
                getCoin = Random.Range(1, 10);
                amoutHaveCoin += getCoin;

                break;
            case PickObject.CoinType.C100:
                getCoin = Random.Range(11, 100);
                amoutHaveCoin += getCoin;

                break;
            case PickObject.CoinType.C500:
                getCoin = Random.Range(101, 500);
                amoutHaveCoin += getCoin;

                break;
            case PickObject.CoinType.C1000:
                getCoin = Random.Range(501, 1000);
                amoutHaveCoin += getCoin;

                break;
        }

    }

    void PickConsume(PickObject pObj)
    {

    }
}
