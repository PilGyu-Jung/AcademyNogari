using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelect : MonoBehaviour
{
    public MainMenu mainMenu;
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
        for (int i = 0; i < slots_Hero.Count; i++)
        {
            SetHeroInSlot(HeroManager.Instance.HeroList[i], slots_Hero[i]);
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

    public void CheckOff()
    {
        foreach (Transform item in slots_Hero)
        {
            item.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
    }

    void HeroStageON()
    {
        mainMenu.StageOn();
    }

    public void OnHeroBtnClick()
    {
        Invoke("HeroStageON", 0.2f);
    }
}
