using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    public enum ItemType { COIN, CONSUME, BUFF };
    public ItemType iType;
    public enum CoinType { C10,C100,C500,C1000};
    public CoinType cType;


    //PlayerController p_controller;
    //PlayerInterAction p_interaction;

    private void Start()
    {
        //p_controller = FindObjectOfType<PlayerController>();
        //p_interaction = FindObjectOfType<PlayerInterAction>();
    }

    void Pickthis()
    {
        switch (this.iType)
        {
            case ItemType.COIN:
                //CoinManager.Instance.
                break;
            case ItemType.CONSUME:

                break;
            case ItemType.BUFF:

                break;
        }

    }
}
