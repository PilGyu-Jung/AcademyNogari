using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
            monsterAttack(isattack);
        }
        else
        {
            Debug.Log("Distance Error!");
        }
    }


    IEnumerator attackCoroutine(float time)
    {
        WaitForSeconds ws = new WaitForSeconds(time);
        while(this.isattack)
        {
            Debug.Log("attack!");
            yield return ws;
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

    void monsterAttack(bool attacking)
    {
        if (!attacking)
            return;
        StartCoroutine(attackCoroutine(attackBetTime));
    }

    void DetectingUnits(State m_State)
    {
        if (m_State == State.IDLE || m_State == State.CHASE)
            return;
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
        StartCoroutine(UpdatePath());

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
        DetectingUnits(curState);
        DistanceChangeState(distance,hasTarget);

    }
}
