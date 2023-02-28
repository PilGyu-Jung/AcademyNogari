using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 position_origin;
    public Camera cur_camera;
    
    [SerializeField]
    float distance_target;


    private void Start()
    {
        distance_target = Vector3.Distance(transform.position, targetTransform.position);
        //position_origin = transform.position;
    }

    private void Update()
    {
        transform.position
            = new Vector3(targetTransform.position.x, position_origin.y,
                targetTransform.position.z + position_origin.z);
        FocusingTarget();
    }

    void FocusingTarget()
    {
        cur_camera.transform.LookAt(targetTransform);
    }
}
