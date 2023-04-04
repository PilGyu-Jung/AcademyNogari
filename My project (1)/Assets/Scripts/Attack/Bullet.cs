using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime_bullet;
    public float rotateSpeed;
    public float bulletForce;
    public float howitz_height;

    public bool isnonLine;
    public bool ishowitzer;

    float time;
    [SerializeField]
    float duration;
    float lineT;
    float heightT;
    float rotateX_T;
    float height;
    float rotateX;

    Rigidbody rb;
    //Att_Projectile att_prj;
    Transform targetTransform;
    [SerializeField]
    Vector3 startPosition;
    [SerializeField]
    Vector3 endPosition;
    Vector3 middlePosition;
    [SerializeField]
    AnimationCurve curve_posY;
    [SerializeField]
    AnimationCurve curve_rotX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //att_prj = transform.parent.GetComponent<Att_Projectile>();
        targetTransform = FindObjectOfType<PlayerController>().transform;
        Destroy(gameObject, lifetime_bullet);
        startPosition = transform.position;
        endPosition = targetTransform.position;
        middlePosition 
            = new Vector3((startPosition.x + endPosition.x) / 2, howitz_height, (startPosition.z + endPosition.z) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        nonLine_bulletmove(isnonLine);
        howitzer_bulletmove(ishowitzer);
        time += Time.deltaTime;
    }

    public void nonLine_bulletmove(bool nonLine)
    {
        if (!nonLine)
            return;

        transform.LookAt(targetTransform);
        rb.velocity = (targetTransform.position - transform.position).normalized * bulletForce;

    }

    void howitzer_bulletmove(bool howitzer)
    {
        if (!howitzer)
            return;

        if (time < duration) // time 이 duration 기간동안.
        {
            time += Time.deltaTime;
            lineT = time / duration; // lineT는 time을 duration 만큼 쪼갠다.
            heightT = curve_posY.Evaluate(lineT); // 가로축이 시간인 posY 애니메이션 커브에 heightT 를 대입.
            rotateX_T = curve_rotX.Evaluate(lineT);

            height = Mathf.Lerp(0.0f, howitz_height, heightT);
            rotateX = Mathf.Lerp(3 * howitz_height, 0f, rotateX_T); // 미사일의 포물선 각도 변화.

            //Debug.Log($"rotateX:{ rotateX} y pos:{ transform.position.y}");
            transform.position = Vector3.Lerp(startPosition, endPosition, lineT) + new Vector3(0f, height, 0f);
            transform.LookAt(new Vector3(endPosition.x, endPosition.y + rotateX - 0.2f, endPosition.z));

        }
        else // duration 이 끝나면 endPosition 에서 멈춤.
            transform.position = endPosition;
    }
}
