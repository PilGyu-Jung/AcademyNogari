using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Entity
{
    public List<GameObject> list_unit;
    public Transform spawnPoint;
    public Team team;
    public float coolT;
    float time;

    IEnumerator coroutineA;
    

    void Start()
    {
        list_unit = new List<GameObject>();
        coroutineA = SpawnUnits(time);
        StartCoroutine(coroutineA);
    }

    void Update()
    {
            
    }

    void AddUnitsInList(int code_unit)
    {
        list_unit.Add(UnitManager.Instance.unitList.Find(x => x.unit_code == code_unit).unit_prefab);
    }

    IEnumerator SpawnUnits(float cool)
    {
        WaitForSeconds ws = new WaitForSeconds(cool);

        while(!isdead)
        {
            foreach (GameObject unit in list_unit)
            {
                Instantiate(unit,spawnPoint);
            }

            yield return ws;
        }
    }
}
