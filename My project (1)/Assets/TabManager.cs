using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject itemShop;
    public GameObject equipment;

    public bool isOn_inv;
    public bool isOn_shop;
    public bool isOn_eqp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputTabKey();
        visualizeTab(inventory,isOn_inv);
        visualizeTab(itemShop, isOn_shop);
        visualizeTab(equipment, isOn_eqp);

    }

    void InputTabKey()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isOn_inv = !isOn_inv;
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            isOn_shop = !isOn_shop;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            isOn_eqp = !isOn_eqp;
        }
    }

    void visualizeTab(GameObject tab,bool visual)
    {
        if (visual == true)
            tab.SetActive(true);
        else
            tab.SetActive(false);
    }
}
