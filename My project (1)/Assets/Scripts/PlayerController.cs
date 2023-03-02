using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public float recovery_Stamina;
    public float decrease_Stamina;
    public float stamina;
    public float time_recovery_SP;

    public bool isMove;
    public bool isrecoverSP;

    public float cur_stamina;

    public Collider MeleeCol;
    public GameObject Playermodel;
    public enum AttackType{ MELEE,PROJECTILE,HITSCAN };
    public AttackType attackType;

    float origin_recovery_Stamina;
    float origin_decrease_Stamina;

    [SerializeField]
    float time_a;
    float time;
    float h;
    float v;
    float rayDistance;
    RaycastHit hit;
    Ray ray;
    Camera m_camera;
    Plane plane;

    Vector3 lookPoint;
    Vector3 moveDirection;
    IEnumerator coroutineA;

    private void Awake()
    {
        origin_decrease_Stamina = decrease_Stamina;
        origin_recovery_Stamina = recovery_Stamina;

    }
    // Start is called before the first frame update
    void Start()
    {
        cur_stamina = stamina;
        m_camera = Camera.main;
        plane = new Plane(Vector3.up, Vector3.zero + new Vector3(0, 1f, 0));
        coroutineA = PA_MeleeAttack(attackBetTime);
    }

    // Update is called once per frame
    void Update()
    {
        time_a += Time.deltaTime;
        ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray,out rayDistance))
        {
            lookPoint = ray.GetPoint(rayDistance);
        }
        Debug.DrawLine(ray.origin, lookPoint, Color.red);
        InputDirection();
        InputAction();
        focusPoint();

        if (isattack && time_a >= attackBetTime)
        { // tima_a가 공격 사이시간보다 크고 isattack 이 true 가 되면 공격.
            PlayerAttack(attackType);
            time_a = 0f;
        }
    }

    void InputDirection()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.forward * v + transform.right * h).normalized;
        changeRateStamina();

        if (h != 0 || v != 0)
            isMove = true;
        else
            isMove = false;

        if(isrun && isMove && cur_stamina > 0 && !isrecoverSP)
        {
            transform.Translate(moveDirection * speed_Run, Space.World);
        }
        else
        {
            transform.Translate(moveDirection * speed_Walk, Space.World);
        }

    }

    void changeRateStamina()
    {
        // 스태미나 최대 최소일때 회복량 제한, 범위내일때 정상 회복량.
        if (cur_stamina >= stamina)
        { // 스태미나가 최대값이상이 될시 회복량 0으로 제한.
            cur_stamina = stamina;
            recovery_Stamina = 0;

        }
        else if (cur_stamina <= 0)
        { // 스태미나가 0이하로 떨어지면 isrecoverSP가 true. 감소량 0.
            isrecoverSP = true;
            cur_stamina = 0;
            decrease_Stamina = 0;
        }
        else
        { // 평상시에는 원래 증감값 유지.
            recovery_Stamina = origin_recovery_Stamina;
            decrease_Stamina = origin_decrease_Stamina;
        }

        // shift를 누르고 움직일때 SP감소.
        if (isMove && isrun)
            cur_stamina -= Time.deltaTime * decrease_Stamina;
        else
        { // 쉬프트를 누르지 않고 움직일때 SP회복
            if(isrecoverSP)
            { // isrecoverSP가 true일시 time이 올라감.
                time += Time.deltaTime;

                if(time < time_recovery_SP)
                { // time이 회복제한시간이하면 회복량은 0.
                    isrecoverSP = true;
                    recovery_Stamina = 0;
                }
                else
                { // time이 회복제한시간을 넘길시 원래 회복량이 되고 isrecover은 다시 false, time은 다시 0으로.
                    isrecoverSP = false;
                    time = 0;
                    recovery_Stamina = origin_recovery_Stamina;
                    cur_stamina += Time.deltaTime * recovery_Stamina;
                }
            }
            else
            { // SP가 0이하가 아니므로 원래 회복량을 유지함.
                cur_stamina += Time.deltaTime * recovery_Stamina;
            }

        }

    }
    void InputAction()
    {
        isattack = Input.GetMouseButtonDown(0);
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(coroutineA);
        }
        //isattack = Input.GetMouseButton(0);

        ///<summary>
        /// isrecover이 false 일 경우에만 leftshift가 isrun을 true로 만들어줌.
        /// </summary>
        isrun = !isrecoverSP ? Input.GetKey(KeyCode.LeftShift) : false;
    }
    void focusPoint()
    { // 플레이어 오브젝트는 유지하고, 모델만 마우스 포인터쪽으로 회전.
        Playermodel.transform.LookAt(lookPoint);
    }

    void PlayerAttack(AttackType type)
    { // 공격 타입에 따라서 다른 공격 코루틴이 실행됨.
        switch (type)
        {
            case AttackType.MELEE:
                StartCoroutine(coroutineA);
                break;
            case AttackType.PROJECTILE:

                break;
            case AttackType.HITSCAN:

                break;
        }
    }

    IEnumerator PA_MeleeAttack(float t)
    {
        WaitForSeconds ws = new WaitForSeconds(t);
        while(true)
        {
            Debug.Log("Player Melee Attacking!");
            MeleeCol.enabled = true;
            yield return new WaitForSeconds(.2f);
            MeleeCol.enabled = false;
            time_a = 0f;
            yield return ws;
        }
    }
 
    
}
