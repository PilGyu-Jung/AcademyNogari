using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public float recovery_Stamina;
    public float decrease_Stamina;
    public float stamina;

    public bool isMove;
    public float cur_stamina;

    float h;
    float v;
    float rayDistance;
    RaycastHit hit;
    Ray ray;
    Camera m_camera;
    Plane plane;

    Vector3 lookPoint;
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

        if(!isrun)
            transform.Translate(h * speed_Walk, 0, v * speed_Walk,Space.World);
        else
            transform.Translate(h * speed_Run, 0, v * speed_Run, Space.World);

    }

    void changeRateStamina()
    {
        if (cur_stamina >= stamina)
            cur_stamina = stamina;

        if (isMove && isrun)
            cur_stamina -= Time.deltaTime * decrease_Stamina;
        else
            cur_stamina += Time.deltaTime * recovery_Stamina;

    }
    void InputAction()
    {
        isattack = Input.GetMouseButton(0);
        isrun = Input.GetKey(KeyCode.LeftShift);
    }
    void focusPoint()
    {
        transform.LookAt(lookPoint);
    }
}
