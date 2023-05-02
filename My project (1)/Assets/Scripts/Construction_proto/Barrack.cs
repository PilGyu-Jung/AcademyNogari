using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Entity
{
    public List<GameObject> list_unit;
    public Transform spawnPoint;
    public Team team;
    public Material[] mat_team;
    public float coolT;
    //float time;

    IEnumerator coroutineA;

    private void Awake()
    {

    }
    void Start()
    {
        list_unit = new List<GameObject>();
        coroutineA = SpawnUnits(coolT);
        StartCoroutine(coroutineA);
        //ChangeToTeamColor(this.GetComponent<Barrack>());
        if (team == Team.B)
        {
            AddUnitsInList("0");
            AddUnitsInList("1");
        }
        else if (team == Team.A)
            AddUnitsInList("00");
    }

    void Update()
    {
        coolT = LevelManager.Instance.currentLevel.termOfSpawn;
    }

    void ChangeToTeamColor(Entity target)
    {
        if (team == Team.A )
        {
            target.baseRenderer.material.color = Color.blue;
        }
        else if(team == Team.B)
        {
            target.baseRenderer.material.color = Color.red;
        }
        else
        {
            Debug.Log(gameObject.name+": team type error!");
        }
    }

    void AddUnitsInList(string code_unit)
    {
        list_unit.Add(UnitManager.Instance.unitList.Find(x => x.unit_code == code_unit).unit_prefab);
    }

    void DeleteUnitInList(string code_unit)
    {
        list_unit.Remove(UnitManager.Instance.unitList.Find(x => x.unit_code == code_unit).unit_prefab);
    }


    //void AddUnitsInList(string name_unit)
    //{
    //    list_unit.Add(UnitManager.Instance.unitList.Find(x => x.unit_name == name_unit).unit_prefab);
    //}

    IEnumerator SpawnUnits(float cool)
    {
        WaitForSeconds ws = new WaitForSeconds(cool);

        while(!isdead)
        {
            foreach (GameObject unit in list_unit)
            {
                //ChangeToTeamColor(unit.GetComponent<Entity>());
                Instantiate(unit,spawnPoint.position,Quaternion.identity);
            }

            yield return ws;
        }
    }
}
