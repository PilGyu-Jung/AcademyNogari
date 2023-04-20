using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabManager : MonoBehaviour
{
    private static TabManager instance = null;

    public GameObject inventory;
    public GameObject itemShop;
    public GameObject equipment;

    public bool isOn_inv;
    public bool isOn_shop;
    public bool isOn_eqp;

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
