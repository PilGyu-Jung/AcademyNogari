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

    float origin_recovery_Stamina;
    float origin_decrease_Stamina;

    [SerializeField]
    float time;
    float h;
    float v;
    float rayDistance;
    RaycastHit hit;
    Ray ray;
    Camera m_camera;
    Plane plane;

    Vector3 lookPoint;

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
    }

    // Update is called once per frame
    void Update()
    {
        ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray,out rayDistance))
        {
            lookPoint = ray.GetPoint(rayDistance);
        }
        Debug.DrawLine(ray.origin, lookPoint, Color.red);
        InputDirection();
        InputAction();
        focusPoint();
    }

    void InputDirection()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        changeRateStamina();

        if (h != 0 || v != 0)
            isMove = true;
        else
            isMove = false;

        if(isrun && isMove && cur_stamina > 0 && !isrecoverSP)
        {
            transform.Translate(h * speed_Run, 0, v * speed_Run, Space.World);
        }
        else
        {
            transform.Translate(h * speed_Walk, 0, v * speed_Walk, Space.World);
        }
        //if(!isrun)
        //    transform.Translate(h * speed_Walk, 0, v * speed_Walk,Space.World);
        //else 
        //    transform.Translate(h * speed_Run, 0, v * speed_Run, Space.World);

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
        isattack = Input.GetMouseButton(0);
        ///<summary>
        /// isrecover�� false �� ��쿡�� leftshift�� isrun�� true�� �������.
        /// </summary>
        isrun = !isrecoverSP ? Input.GetKey(KeyCode.LeftShift) : false;
    }
    void focusPoint()
    {
        transform.LookAt(lookPoint);
    }
}
