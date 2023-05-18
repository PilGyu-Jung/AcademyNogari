using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl_Unit : MonoBehaviour
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

    public void Anim_moving(bool ismove)
    {
        if (!ismove)
        {
            animator.SetBool("isMove", false);
            return;
        }

        animator.SetBool("isMove", ismove);
    }

    public void Anim_attack()
    {
        animator.SetTrigger("Attack");
    }
}
