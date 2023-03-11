using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomLootingObject : MonoBehaviour
{
    public List <GameObject> dropArray_Obj;
    public List <int>        dropArray_Rate;
    [SerializeField]
    private SerializableDictionary<int, GameObject> dic_dropCoinNum;
    public int          targetnum;
    public int          num_Itemdrop;
    public float        range_drop;

    Items chooseItem;
    Entity dropTarget;
    public GameObject lootSelectionObj;
    public List<string> list_Item = new List<string>();
    public List<string> list_coin = new List<string>();
    //[SerializeField]
    //public Dictionary<GameObject, int> Dic_dropObjRate;
    int sumRate;

    private void Start()
    {
        //dropTarget.OnDead += (dropObjects, dropRates) => DropItems(dropObjects, dropRates);
        AddItemList(dropArray_Obj, dropArray_Rate);
        AddCoinList(dic_dropCoinNum);
        RandomLooting(list_Item, sumRate);
        RandomPosDrop(range_drop, lootSelectionObj);
    }


    void AddItemList(List<GameObject> d_Objects, List<int> d_Rates)
    {
        if (d_Objects.Count != d_Rates.Count)
        { // 오브젝트와 확률의 개수를 비교함.
            Debug.Log("아이템과 확률의 개수가 불일치합니다.");
            return;
        }
        for (int i = 0; i < d_Rates.Count; i++)
        { // 확률의 개수를 가져옴.
            for (int j = 0; j < d_Rates[i]; j++)
            { // 확률에 들어있는 숫자요소만큼 리스트에 이름을 넣음.
                list_Item.Add(d_Objects[i].name);
            }
        }
        foreach (int n in d_Rates)
        {
            sumRate += n;
        }
        //targetnum = Random.Range(0, sumRate);
    }

    void AddCoinList(SerializableDictionary<int, GameObject> SD_coin)
    {
        for (int i = 0; i < SD_coin.Count; i++)
        {
            for (int j = 0; j < SD_coin.Keys.ToList()[i]; j++)
            {
                //list_coin.Add(SD_coin.Values.ToList());
                list_coin.Add(SD_coin.Values.ToList()[i].name);
            }
        }
    }

    void RandomLooting(List<string> dropItemlist,int sum)
    {
        targetnum = Random.Range(0, sum);

        for (int i = 0; i < dropItemlist.Count; i++)
        {
            if(i == targetnum)
            { //targetnum인 인덱스에 해당하는 아이템 이름(string) 추출.
                // 아이템 이름과 같은 아이템을 dropArray_Obj에서 탐색.
                // 탐색한 아이템을 lootSelectionObj 변수에 넣음.
                lootSelectionObj = dropArray_Obj.Where(obj => obj.name == dropItemlist[i]).SingleOrDefault();
            }
        }
    }

    void RandomPosDrop(float r,GameObject obj)
    {
        float randX = Random.Range(-r, r);
        float randZ = Random.Range(-r, r);
        Vector3 randPos = new Vector3(randX, 0, randZ);
        Instantiate(obj, (transform.position + randPos), Quaternion.identity);
    }

}
