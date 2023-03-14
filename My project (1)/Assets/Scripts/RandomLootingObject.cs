using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomLootingObject : MonoBehaviour
{
    [SerializeField]
    private SerializableDictionary<int, GameObject> dic_dropCoinNum;
    [SerializeField]
    private SerializableDictionary<int, GameObject> dic_dropItemRate;

    public int          targetnum;
    public int          num_Itemdrop;
    public float        range_drop;

    int sumRate;

    Items chooseItem;
    Entity dropTarget;
    public GameObject lootSelectionObj;
    public GameObject lootSelectionCoin;
    public List<string> list_Item = new List<string>();
    public List<string> list_coin = new List<string>();
    

    private void Start()
    {
        /*
        //dropTarget.OnDead += (dropObjects, dropRates) => DropItems(dropObjects, dropRates);
        //AddItemList1(dic_dropItemRate);
        //AddCoinList(dic_dropCoinNum);
        //RandomLooting(list_Item, sumRate);
        //RandomPosDrop(range_drop, lootSelectionObj);
        //SpawnCoinList(list_coin);
        */
        //DroplootingItem();
        //DroplootingCoin();
    }


    public void DroplootingItem()
    {
        AddItemList1(dic_dropItemRate);
        for (int i = 0; i < num_Itemdrop; i++)
        {
            RandomLooting(list_Item, sumRate);
        }

    }

    public void DroplootingCoin()
    {
        AddCoinList(dic_dropCoinNum);
        SpawnCoinList(list_coin);

    }

    void AddItemList0(List<GameObject> d_Objects, List<int> d_Rates)
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

    void AddItemList1(SerializableDictionary<int,GameObject>SD_item)
    {
        for (int i = 0; i < SD_item.Count; i++)
        { // Item Dictionary의 개수를 i
            for (int j = 0; j < SD_item.Keys.ToList()[i]; j++)
            { // Item Dictionary Key 에 들어있는 int를 추출한 것이 j
                list_Item.Add(SD_item.Values.ToList()[i].name);
            } // list_item에 Item Dictionary 의 value이름을 추가한다.
            sumRate += SD_item.Keys.ToList()[i];
        } // sumRate에 Key값들을 더해준다.

    }
    void AddCoinList(SerializableDictionary<int, GameObject> SD_coin)
    {
        for (int i = 0; i < SD_coin.Count; i++)
        { // Coin Dictionary 의 개수를 i
            for (int j = 0; j < SD_coin.Keys.ToList()[i]; j++)
            { //Coin Dictionary Key에 들어있는 int를 추출한 것이 j
                list_coin.Add(SD_coin.Values.ToList()[i].name);
            } // list_coin에 Coin Dictionary 의 value이름을 추가한다.
        }
    }

    void SpawnCoinList(List<string> dropCoinlist)
    {//dropcoinlist의 개수를 i
        for (int i = 0; i < dropCoinlist.Count; i++)
        {// dropcoinlist i번째에있는 요소와 Coin dictionary가 같은 이름을 공유하는 Gameobject를 찾아 lootSelectionCoin에 넣는다.
            lootSelectionCoin = dic_dropCoinNum.Values.Where(obj => obj.name == dropCoinlist[i]).SingleOrDefault();
            RandomPosDrop(range_drop, lootSelectionCoin);
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
                lootSelectionObj = dic_dropItemRate.Values.Where(obj => obj.name == dropItemlist[i]).SingleOrDefault();
            }
        }
        RandomPosDrop(range_drop, lootSelectionObj);
    }

    void RandomPosDrop(float r,GameObject obj)
    {
        float randX = Random.Range(-r, r);
        float randZ = Random.Range(-r, r);
        Vector3 randPos = new Vector3(randX, 0, randZ);
        Instantiate(obj, (transform.position + randPos), Quaternion.identity);
    }

}
