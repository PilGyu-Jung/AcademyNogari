using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EquipmentSlot : DroppableSlot
{
    public enum Equiptype { HELMET,ARMOR,AMULET1,AMULET2,WEAPON1,WEAPON2,GLOVES,BOOTS};
    public Equiptype eType;

    [SerializeField]
    TextMeshProUGUI slotText;

    // Start is called before the first frame update
    void Start()
    {
        switch (eType)
        {
            case Equiptype.HELMET:
                slotText.text = "Helmet";
                break;
            case Equiptype.ARMOR:
                slotText.text = "Armor";

                break;
            case Equiptype.AMULET1:
                slotText.text = "Amulet";

                break;
            case Equiptype.AMULET2:
                slotText.text = "Amulet";

                break;
            case Equiptype.WEAPON1:
                slotText.text = "Weapon1";

                break;
            case Equiptype.WEAPON2:
                slotText.text = "Weapon2";

                break;
            case Equiptype.GLOVES:
                slotText.text = "Gloves";

                break;
            case Equiptype.BOOTS:
                slotText.text = "Boots";

                break;
        }
        if(transform.childCount > 1)
        {

        }
        else
        {

        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
