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
            //monsterAttack(isattack);
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

                Destroy(this.gameObject,0.3f);
                break;
            }
        }
    }

    IEnumerator AttackCoroutine(float time)
    {
        WaitForSeconds ws = new WaitForSeconds(time);
        while(this.isattack)
        {
            Debug.Log("attack!");
            yield return ws;
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
                //Debug.Log("Monster is attacking! target :{0}", transform_Target.GetComponentInParent<GameObject>().name);
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

    //void monsterAttack(bool attacking)
    //{
    //    if (!attacking)
    //        return;
    //    StartCoroutine(AttackCoroutine(attackBetTime));
    //}

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
    // Start is called before the first frame update
    private void Awake()
    {
        transform_Target = transform_EnemyBase;

    }
    void Start()
    {
        isdead = false;
        hasTarget = false;
        monsterAgent = GetComponent<NavMeshAgent>();
        objRL = GetComponent<RandomLootingObject>();

        StartCoroutine(UpdatePath());
        StartCoroutine(MonsterDead());
        m_dead += objRL.DroplootingCoin;
    }

    // Update is called once per frame
    void Update()   
    {
        #region attackCoroutineTest
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    isAttack = true;
        //    monsterAttack(isAttack);
        //}
        //else if(Input.GetKeyUp(KeyCode.A))
        //{
        //    isAttack = false;
        //}
        #endregion
        DetectingUnits();
        DistanceChangeState(distance,hasTarget);
        if(hp <= 0)
        {
            isdead = true;
        }
    }
}
