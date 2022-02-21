using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public Animator anim;
    
    public void IsGrounded(bool onGround)
    {
        anim.SetBool("Grounded", onGround);
    }

    public void WalkAnimation(float moveSpeedX, float moveSpeedZ)
    {
        if (moveSpeedX < 0)
        {
            anim.SetBool("wFront", false);
            anim.SetBool("wBack", true);
        }
        else if (moveSpeedX > 0 || moveSpeedZ != 0)
        {
            anim.SetBool("wFront", true);
            anim.SetBool("wBack", false);
        }
        else
        {
            anim.SetBool("wFront", false);
            anim.SetBool("wBack", false);
        }
    }

    public void Attack()
    {
        anim.SetTrigger("attack");
    }
}
