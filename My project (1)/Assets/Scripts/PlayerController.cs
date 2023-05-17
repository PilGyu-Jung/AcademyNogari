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

    public MeleeWeapon weapon_melee;
    public GameObject Playermodel;
    public enum AttType{ MELEE,PROJECTILE,HITSCAN };
    public AttType attackType;
    public AnimControl_Player anim_Player;

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

    enum Player_Direction { UP,UP_RIGHT,RIGHT,RIGHT_DOWN,DOWN,DOWN_LEFT,LEFT,LEFT_UP};
    
    [SerializeField]
    Player_Direction DirectionType;

    Vector3 lookPoint;
    Vector3 moveDirection;
    Vector2 v2_hv;
    float degree;
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
        { // tima_a�� ���� ���̽ð����� ũ�� isattack �� true �� �Ǹ� ����.
            PlayerAttack(attackType);
            time_a = 0f;
        }
        //else
        //{
        //    StopCoroutine(coroutineA);
        //}
    }

    void InputDirection()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        moveDirection = (transform.forward * v + transform.right * h).normalized;
        changeRateStamina();

        if (h != 0 || v != 0)
        {
            isMove = true;
        }
        else
        {
            isMove = false;

        }

        if (isrun && isMove && cur_stamina > 0 && !isrecoverSP)
        {
            transform.Translate(moveDirection * speed_Run, Space.World);
        }
        else
        {
            transform.Translate(moveDirection * speed_Walk, Space.World);
        }
        WhichDirection(moveDirection);
        anim_Player.Anim_moving(Mathf.Abs(Playermodel.transform.rotation.eulerAngles.y - degree), isMove);

    }

    void WhichDirection(Vector3 dirV3)
    {
        if(dirV3 == new Vector3(1,0,0))
        {
            DirectionType = Player_Direction.RIGHT;
            degree = 360f;
        }
        else if (dirV3 == new Vector3(-1, 0, 0))
        {
            DirectionType = Player_Direction.LEFT;
            degree = 180f;
        }
        else if (dirV3 == new Vector3(0, 0, 1))
        {
            DirectionType = Player_Direction.UP;
            degree = 90f;
        }
        else if (dirV3 == new Vector3(0, 0, -1))
        {
            DirectionType = Player_Direction.DOWN;
            degree = 270f;
        }
        else if (Vector3.Distance(dirV3,new Vector3(0.71f, 0, 0.71f)) <= 0.1f)
        {   
            DirectionType = Player_Direction.UP_RIGHT;
            degree = 45f;
        }
        else if (Vector3.Distance(dirV3, new Vector3(0.71f, 0, -0.71f)) <= 0.1f)
        {
            DirectionType = Player_Direction.RIGHT_DOWN;
            degree = 135f;
        }
        else if (Vector3.Distance(dirV3, new Vector3(-0.71f, 0, -0.71f)) <= 0.1f)
        {
            DirectionType = Player_Direction.DOWN_LEFT;
            degree = 225f;
        }
        else if (Vector3.Distance(dirV3, new Vector3(-0.71f, 0, 0.71f)) <= 0.1f)
        {
            DirectionType = Player_Direction.LEFT_UP;
            degree = 315f;
        }
        else
        {
            degree = 0f;
        }
    }

    void Anim_moving(float yAngle)
    {
        //if(yAngle - degree <= 45f || yAngle - degree >= 360f)
    }
    void changeRateStamina()
    {
        // ���¹̳� �ִ� �ּ��϶� ȸ���� ����, �������϶� ���� ȸ����.
        if (cur_stamina >= stamina)
        { // ���¹̳��� �ִ밪�̻��� �ɽ� ȸ���� 0���� ����.
            cur_stamina = stamina;
            recovery_Stamina = 0;

        }
        else if (cur_stamina <= 0)
        { // ���¹̳��� 0���Ϸ� �������� isrecoverSP�� true. ���ҷ� 0.
            isrecoverSP = true;
            cur_stamina = 0;
            decrease_Stamina = 0;
        }
        else
        { // ���ÿ��� ���� ������ ����.
            recovery_Stamina = origin_recovery_Stamina;
            decrease_Stamina = origin_decrease_Stamina;
        }

        // shift�� ������ �����϶� SP����.
        if (isMove && isrun)
            cur_stamina -= Time.deltaTime * decrease_Stamina;
        else
        { // ����Ʈ�� ������ �ʰ� �����϶� SPȸ��
            if(isrecoverSP)
            { // isrecoverSP�� true�Ͻ� time�� �ö�.
                time += Time.deltaTime;

                if(time < time_recovery_SP)
                { // time�� ȸ�����ѽð����ϸ� ȸ������ 0.
                    isrecoverSP = true;
                    recovery_Stamina = 0;
                }
                else
                { // time�� ȸ�����ѽð��� �ѱ�� ���� ȸ������ �ǰ� isrecover�� �ٽ� false, time�� �ٽ� 0����.
                    isrecoverSP = false;
                    time = 0;
                    recovery_Stamina = origin_recovery_Stamina;
                    cur_stamina += Time.deltaTime * recovery_Stamina;
                }
            }
            else
            { // SP�� 0���ϰ� �ƴϹǷ� ���� ȸ������ ������.
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
            weapon_melee.attackbox.enabled = false;
        }
        //isattack = Input.GetMouseButton(0);

        ///<summary>
        /// isrecover�� false �� ��쿡�� leftshift�� isrun�� true�� �������.
        /// </summary>
        isrun = !isrecoverSP ? Input.GetKey(KeyCode.LeftShift) : false;
    }
    void focusPoint()
    { // �÷��̾� ������Ʈ�� �����ϰ�, �𵨸� ���콺 ������������ ȸ��.
        Playermodel.transform.LookAt(new Vector3(lookPoint.x, transform.position.y, lookPoint.z));
        //Debug.Log(Playermodel.transform.rotation.eulerAngles.y);
    }

    void PlayerAttack(AttType type)
    { // ���� Ÿ�Կ� ���� �ٸ� ���� �ڷ�ƾ�� �����.
        switch (type)
        {
            case AttType.MELEE:
                StartCoroutine(coroutineA);
                break;
            case AttType.PROJECTILE:

                break;
            case AttType.HITSCAN:

                break;
        }
    }

    IEnumerator PA_MeleeAttack(float t)
    {
        WaitForSeconds ws = new WaitForSeconds(t);
        while(true)
        {
            Debug.Log("Player Melee Attacking!");
            anim_Player.Anim_attack(1);
            weapon_melee.attackbox.enabled = true;
            yield return new WaitForSeconds(.2f);
            weapon_melee.attackbox.enabled = false;
            time_a = 0f;
            yield return ws;
        }
    }
 
    
}
