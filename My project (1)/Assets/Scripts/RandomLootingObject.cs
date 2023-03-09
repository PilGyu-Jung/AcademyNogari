using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLootingObject : MonoBehaviour
{
    public GameObject[] dropArray_Obj;
    public int[]        dropArray_Rate;
    public int          targetnum;

    Entity dropTarget;
    public GameObject lootSelectionObj;
    public List<string> list_Item = new List<string>(); 
    int sumRate;

    private void Start()
    {
        //dropTarget.OnDead += (dropObjects, dropRates) => DropItems(dropObjects, dropRates);
    }

    void AddItemList(GameObject[] d_Objects, int[] d_Rates)
    {
        if (d_Objects.Length != d_Rates.Length)
        {
            Debug.Log("아이템과 확률의 개수가 불일치합니다.");
            return;
        }
        for (int i = 0; i < d_Rates.Length; i++)
        {
            for (int j = 0; j < d_Rates[i]; j++)
            {
                list_Item.Add(d_Objects[i].name);
            }
        }
        foreach (int n in d_Rates)
        {
            sumRate += n;
        }
        //targetnum = Random.Range(0, sumRate);
    }

    void RandomLooting(List<string> dropItemlist,int sum)
    {
        targetnum = Random.Range(0, sum);

        for (int i = 0; i < dropItemlist.Count; i++)
        {
            if(i == targetnum)
            { //targetnum인 인덱스에 해당하는 아이템 이름 추출.
                //lootSelectionObj = dropItemlist[i];
                //ItemManager.Instance.ItemList.IndexOf(lootSelectionObj.name);
            }
        }
       

        //targetnum 와 같은 IList 인덱스값에 있는 아이템이름.
        //lootSelectionObj = List.FindIndex(targetnum);
    }
}
