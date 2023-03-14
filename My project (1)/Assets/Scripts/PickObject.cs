using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    public enum ObjectType { COIN, ITEM };
    public ObjectType objType;


    //PlayerController p_controller;
    //PlayerInterAction p_interaction;

    private void Start()
    {
        //p_controller = FindObjectOfType<PlayerController>();
        //p_interaction = FindObjectOfType<PlayerInterAction>();
    }

    void Pickthis()
    {
        switch (this.objType)
        {
            case ObjectType.COIN:
                //CoinManager.Instance.
                break;
            case ObjectType.ITEM:

                break;
        }

    }
}
