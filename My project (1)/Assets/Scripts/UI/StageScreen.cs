using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageScreen : MonoBehaviour
{
    public Transform root_StageSlot;
    public Transform prefab_StageSlot;
    public Transform prefab_LevelImage;
    

    [SerializeField]
    int num_stage;
    [SerializeField]
    List<int> num_stageLevels;
    // Start is called before the first frame update
    void Start()
    {
        num_stage = LevelManager.Instance.list_Stage.Count;
        for (int i = 0; i < num_stage; i++)
        {
            num_stageLevels.Add(LevelManager.Instance.list_Stage[i].list_Levels.Count);
        }
        DisplayStageSlots();
        AddLevelIcon_StageSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayStageSlots()
    {
        for (int i = 0; i < num_stage; i++)
        {
            Instantiate(prefab_StageSlot.gameObject, root_StageSlot);
        }
    }

    void AddLevelIcon_StageSlot()
    {
        for (int i = 0; i < num_stage; i++)
        {
            root_StageSlot.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Stage "+ i;
            for (int j = 0; j < num_stageLevels[i]; j++)
            {
                Instantiate(prefab_LevelImage, root_StageSlot.GetChild(i).GetChild(1));
            }
        }
    }

    void PressStageButton()
    {

    }
}
