using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabManager : MonoBehaviour
{
    private static TabManager instance = null;

    public RectTransform inventory;
    public RectTransform itemShop;
    public RectTransform equipment;

    public bool isOn_inv;
    public bool isOn_shop;
    public bool isOn_eqp;

    Vector2 inventory_transform_origin;
    Vector2 itemShop_transform_origin;
    Vector2 equipment_transform_origin;

    public RectTransform popup;

    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_expl;
    public TextMeshProUGUI text_price;

    public static TabManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text_name = popup.GetChild(0).GetComponent<TextMeshProUGUI>();
        text_expl = popup.GetChild(1).GetComponent<TextMeshProUGUI>();
        text_price = popup.GetChild(2).GetComponent<TextMeshProUGUI>();

        inventory_transform_origin = inventory.anchoredPosition;
        itemShop_transform_origin = itemShop.anchoredPosition;
        equipment_transform_origin = equipment.anchoredPosition;


        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputTabKey();
        visualizeTab(inventory,isOn_inv);
        visualizeTab(itemShop, isOn_shop);
        visualizeTab(equipment, isOn_eqp);

    }

    Vector2 WhichTabOriginV2(RectTransform whichTab)
    {
        if(whichTab == inventory)
        {
            return inventory_transform_origin;
        }
        else if(whichTab == itemShop)
        {
            return itemShop_transform_origin;
        }
        else if(whichTab == equipment)
        {
            return equipment_transform_origin;
        }
        else
        {
            Debug.Log("WhichTab Error!");
            return new Vector2(0, 0);
        }

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

    void visualizeTab(RectTransform tab,bool visual)
    {
        if (visual == true)
            tab.anchoredPosition = WhichTabOriginV2(tab);
        else
            tab.anchoredPosition = WhichTabOriginV2(tab) + new Vector2(0, 1300);
    }

    public void hideinvTab()
    {
        isOn_inv = false;
    }

    public void hideeqpTab()
    {
        isOn_eqp = false;
    }

    public void hideshopTab()
    {
        isOn_shop = false;
    }
}
