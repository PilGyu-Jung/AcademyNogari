using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterAction : MonoBehaviour
{
    public int amoutHaveCoin;
    public float radius_absorb;
    public SphereCollider pickItem;
    public PickCoin pickObj;
    public int getCoin;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            pickObj = other.gameObject.GetComponent<PickCoin>();
            GetCoin(pickObj);
            Destroy(other.gameObject);
            Debug.Log(other.gameObject.name);

        }
    }
    void GetCoin(PickCoin pObj)
    {
        switch (pObj.cType)
        {
            case PickCoin.CoinType.C10:
                getCoin = Random.Range(1, 10);
                amoutHaveCoin += getCoin;

                break;
            case PickCoin.CoinType.C100:
                getCoin = Random.Range(11, 100);
                amoutHaveCoin += getCoin;

                break;
            case PickCoin.CoinType.C500:
                getCoin = Random.Range(101, 500);
                amoutHaveCoin += getCoin;

                break;
            case PickCoin.CoinType.C1000:
                getCoin = Random.Range(501, 1000);
                amoutHaveCoin += getCoin;

                break;
        }

    }
}
