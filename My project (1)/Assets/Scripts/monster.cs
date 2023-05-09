using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : Entity
{
    public bool         hasTarget;
    public float        distance;

    [Range(1.0f, 15.0f)]
    public float        radius_Detect;
    [Range(1.0f, 12.0f)]
    public float        radius_Chase;
    [Range(1.0f, 10.0f)]
    public float        radius_Attack;


    Collider[]          cols;
    Barrack[]           barracks;
    NavMeshAgent        monsterAgent;
    Action              m_dead;
    RandomLootingObject objRL;

    [SerializeField]
    LayerMask           m_targetLayer;
    [SerializeField]
    State               curState;
    [SerializeField]
    Team                monsterTeam;

    public Transform    transform_Target;
    public Transform    transform_EnemyBase;
    public Entity       targetEntity;

    GameObject m_hpbar;
    [SerializeField]
    UnityEngine.UI.Slider slider_HP;

    void DistanceChangeState(float d,bool nearTarget)
    {
        if (!nearTarget)
            return;

        if(d > radius_Chase)
        {
            curState = State.IDLE;
            monsterAgent.autoBraking = false;
            monsterAgent.speed = speed_Walk;
            monsterAgent.isStopped = false;
            hasTarget = false;
            isattack = false;
            isrun = false;

        }
        else if(d <= radius_Chase && d > radius_Detect)
        {
            curState = State.CHASE;
            monsterAgent.autoBraking = true;
            monsterAgent.speed = speed_Run;
            monsterAgent.isStopped = false;

            isattack = false;
            isrun = true;


        }
        else if(d <= radius_Detect && d > radius_Attack)
        {
            curState = State.CHASE;
            monsterAgent.autoBraking = true;
            monsterAgent.speed = speed_Run;
            monsterAgent.isStopped = false;

            isattack = false;
            isrun = true;

        }
        else if(d <= radius_Attack)
        {
            curState = State.ATTACK;
            monsterAgent.autoBraking = true;
            monsterAgent.speed = 0;
            monsterAgent.isStopped = true;

            isattack = true;
            isrun = false;
        }
        else
        {
            Debug.Log("Distance Error!");
        }
    }

    IEnumerator MonsterDead()
    {
        float t = .25f;
        WaitForSeconds ws = new WaitForSeconds(t);

        while(true)
        {
            yield return ws;
            if(isdead)
            {
                m_dead();
                StopCoroutine(MonsterDead());
                StopCoroutine(UpdatePath());
                StopCoroutine(Mons_Attack(attackBetTime));

                Destroy(this.gameObject,0.5f);
                break;
            }
        }
    }

    IEnumerator Mons_Attack(float time)
    {
        float t = .25f;
        WaitForSeconds ws = new WaitForSeconds(t);
        WaitForSeconds ws2 = new WaitForSeconds(time);

        while(!isdead)
        {
            if(curState == State.ATTACK)
            {
                Debug.Log("Monster Attack!");
                yield return ws2;
            }
            else
            {
                yield return ws;
            }
        }

    }

    IEnumerator UpdatePath()
    {
        float t = .25f;
        WaitForSeconds ws = new WaitForSeconds(t);

        while (!isdead)
        {
            yield return ws;
            Debug.Log("UpdatePath!");

            if (hasTarget)
            {
                monsterAgent.SetDestination(transform_Target.position);
            }
            else
                monsterAgent.SetDestination(transform_EnemyBase.position);

        }
    }


    void DetectingUnits()
    {

        cols 
            = Physics.OverlapSphere(transform.position, radius_Detect,m_targetLayer);

        foreach(Collider col in cols)
        {
            if(col != null)
            {
                hasTarget = true;
                transform_Target = col.gameObject.transform;
                distance = Vector3.Distance(this.transform.position, transform_Target.position);
            }
            else
            {
                hasTarget = false;
                transform_Target = transform_EnemyBase;
            }
        }
    }

    void SetPosEnemybase()
    {
        if(monsterTeam == Team.A)
        {
            for (int i = 0; i < barracks.Length; i++)
            {
                if(barracks[i].team == Team.B)
                {
                    transform_EnemyBase = barracks[i].gameObject.transform;
                }
            }
        }
        else if(monsterTeam == Team.B)
        {
            for (int i = 0; i < barracks.Length; i++)
            {
                if (barracks[i].team == Team.A)
                {
                    transform_EnemyBase = barracks[i].gameObject.transform;
                }
            }

        }
        else
        {
            Debug.Log(gameObject.name + ": set EnemyBase Postion Error!");
        }
    }

    private void Awake()
    {
        transform_Target = transform_EnemyBase;

    }
    void Start()
    {
        maxHP = hp;
        maxMP = mp;

        m_hpbar = Instantiate(UnitManager.Instance.prefab_HpBar, transform.position, Quaternion.identity, UnitManager.Instance.canvas.transform as RectTransform);
        slider_HP = m_hpbar.GetComponent<UnityEngine.UI.Slider>();

        isdead = false;
        hasTarget = false;
        monsterAgent = GetComponent<NavMeshAgent>();
        objRL = GetComponent<RandomLootingObject>();

        StartCoroutine(UpdatePath());
        StartCoroutine(MonsterDead());
        StartCoroutine(Mons_Attack(attackBetTime));
        m_dead += objRL.DroplootingCoin;
        barracks = FindObjectsOfType<Barrack>();

        SetPosEnemybase();
    }

    // Update is called once per frame
    void Update()   
    {
        slider_HP.value = (float)hp / (float)maxHP;

        if (hp <= 0)
        {
            isdead = true;
            Destroy(m_hpbar,0.1f);
        }
        else
        {
            DetectingUnits();
            DistanceChangeState(distance, hasTarget);

            m_hpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0f, UnitManager.Instance.height_hpBar, 0f));
        }
    }
}
