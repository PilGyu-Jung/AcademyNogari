using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    public enum ItemType { COIN, ITEM };
    public ItemType iType;


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
            case ItemType.ITEM:

                break;
        }

    }
}
