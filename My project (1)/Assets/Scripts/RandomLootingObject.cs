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
            { //targetnum�� �ε����� �ش��ϴ� ������ �̸�(string) ����.
                // ������ �̸��� ���� �������� dropArray_Obj���� Ž��.
                // Ž���� �������� lootSelectionObj ������ ����.
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
