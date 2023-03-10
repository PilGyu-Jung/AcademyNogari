using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomLootingObject : MonoBehaviour/*, ISerializationCallbackReceiver*/
{
    public List <GameObject> dropArray_Obj;
    public List <int>        dropArray_Rate;
    public int          targetnum;
    public int          num_drop;
    public float        range_drop;

    Items chooseItem;
    Entity dropTarget;
    public GameObject lootSelectionObj;
    public List<string> list_Item = new List<string>();
    //[SerializeField]
    //public Dictionary<GameObject, int> Dic_dropObjRate;
    int sumRate;

    private void Start()
    {
        //dropTarget.OnDead += (dropObjects, dropRates) => DropItems(dropObjects, dropRates);
        AddItemList(dropArray_Obj, dropArray_Rate);
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

    //void ISerializationCallbackReceiver.OnBeforeSerialize()
    //{
    //    throw new System.NotImplementedException();
    //}

    //void ISerializationCallbackReceiver.OnAfterDeserialize()
    //{
    //    throw new System.NotImplementedException();
    //}
}
