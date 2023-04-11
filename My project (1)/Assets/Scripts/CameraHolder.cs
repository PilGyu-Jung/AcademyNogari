using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 constructPosition;
    public Vector3 position_origin;
    public Vector3 rotate_Construction;
    public Quaternion origin_rotate;
    public Camera cur_camera;
        
    [SerializeField]
    PlayerController p_controller;
    GridSystem gridSys;

    [SerializeField]
    float distance_target;

    public bool seePlayer;
    public bool seeConstruct;

    private void Start()
    {
        seePlayer = true;
        seeConstruct = false;
        distance_target = Vector3.Distance(transform.position, targetTransform.position);
        //position_origin = transform.position;
        p_controller = FindObjectOfType<PlayerController>();
        gridSys = FindObjectOfType<GridSystem>();
    }

    private void Update()
    {
        if(seePlayer)
        {
            cur_camera.gameObject.transform.rotation = origin_rotate;

            transform.position
                = new Vector3(targetTransform.position.x, position_origin.y,
                    targetTransform.position.z + position_origin.z);
            FocusingTarget();
        }
        else
        {
            transform.position = new Vector3(gridSys.gameObject.transform.position.x + 13.17f, constructPosition.y, constructPosition.z);
            cur_camera.gameObject.transform.rotation = Quaternion.Euler(75.92f, -90, 0);

        }
    }

    void FocusingTarget()
    {
        cur_camera.transform.LookAt(targetTransform);
    }
}
