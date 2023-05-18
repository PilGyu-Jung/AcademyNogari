using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl_Player : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Anim_moving(float _deg,bool ismove)
    {
        if (!ismove)
        {
            animator.SetBool("isMove", false);
            return;
        }

        animator.SetBool("isMove", ismove);
        animator.SetFloat("Deg", _deg);
    }

    public void Anim_attack(int type)
    {
        switch (type)
        {
            case 1:
                animator.SetTrigger("Attack1");
                break;
            case 2:
                animator.SetTrigger("Attack2");
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
            case 4:
                animator.SetTrigger("Attack4");
                break;

        }

    }
    //public void Anim_moving(float x, float y)
    //{
    //    if(Mathf.Approximately(x,0) && Mathf.Approximately(y,0))
    //    {
    //        animator.SetBool("isMove", false);
    //    }
    //    else
    //    {
    //        animator.SetBool("isMove", true);
    //        animator.SetFloat("xDir", x);
    //        animator.SetFloat("yDir", y);
    //    }
    //}
}
