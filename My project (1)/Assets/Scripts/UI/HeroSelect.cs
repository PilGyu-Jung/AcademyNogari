using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelect : MonoBehaviour
{
    public Transform slotRoot_Hero;
    [SerializeField]
    List<Transform> slots_Hero;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotRoot_Hero.childCount; i++)
        {
            slots_Hero.Add(slotRoot_Hero.GetChild(i));
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetHeroInSlot(Hero hero, Transform slot)
    {
        slot.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().enabled = true;
        slot.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = hero.image_Hero;

        slot.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = hero.name_Hero;
        slot.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = hero.desc_Hero;

    }
}
