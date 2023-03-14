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
        { // ������Ʈ�� Ȯ���� ������ ����.
            Debug.Log("�����۰� Ȯ���� ������ ����ġ�մϴ�.");
            return;
        }
        for (int i = 0; i < d_Rates.Count; i++)
        { // Ȯ���� ������ ������.
            for (int j = 0; j < d_Rates[i]; j++)
            { // Ȯ���� ����ִ� ���ڿ�Ҹ�ŭ ����Ʈ�� �̸��� ����.
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
        { // Item Dictionary�� ������ i
            for (int j = 0; j < SD_item.Keys.ToList()[i]; j++)
            { // Item Dictionary Key �� ����ִ� int�� ������ ���� j
                list_Item.Add(SD_item.Values.ToList()[i].name);
            } // list_item�� Item Dictionary �� value�̸��� �߰��Ѵ�.
            sumRate += SD_item.Keys.ToList()[i];
        } // sumRate�� Key������ �����ش�.

    }
    void AddCoinList(SerializableDictionary<int, GameObject> SD_coin)
    {
        for (int i = 0; i < SD_coin.Count; i++)
        { // Coin Dictionary �� ������ i
            for (int j = 0; j < SD_coin.Keys.ToList()[i]; j++)
            { //Coin Dictionary Key�� ����ִ� int�� ������ ���� j
                list_coin.Add(SD_coin.Values.ToList()[i].name);
            } // list_coin�� Coin Dictionary �� value�̸��� �߰��Ѵ�.
        }
    }

    void SpawnCoinList(List<string> dropCoinlist)
    {//dropcoinlist�� ������ i
        for (int i = 0; i < dropCoinlist.Count; i++)
        {// dropcoinlist i��°���ִ� ��ҿ� Coin dictionary�� ���� �̸��� �����ϴ� Gameobject�� ã�� lootSelectionCoin�� �ִ´�.
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
            { //targetnum�� �ε����� �ش��ϴ� ������ �̸�(string) ����.
              // ������ �̸��� ���� �������� dropArray_Obj���� Ž��.
              // Ž���� �������� lootSelectionObj ������ ����.
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
