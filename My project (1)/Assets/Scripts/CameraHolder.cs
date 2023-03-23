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
    float distance_target;
    public bool seePlayer;

    private void Start()
    {
        seePlayer = true;
        distance_target = Vector3.Distance(transform.position, targetTransform.position);
        //position_origin = transform.position;
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
            transform.position = constructPosition;
            cur_camera.gameObject.transform.rotation = Quaternion.Euler(75.92f, -90, 0);

        }
    }

    void FocusingTarget()
    {
        cur_camera.transform.LookAt(targetTransform);
    }
}
