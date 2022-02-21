using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    //Animation called in CharacterMovement script
    
    [SerializeField] private Animator animator;

    public void RunAnimation(float hMove, float vMove)
    {
        if ((hMove < -0.2f || hMove > 0.2f) || (vMove < -0.2f || vMove > 0.2f))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void CatchItem()
    {
        animator.Rebind();
        animator.SetTrigger("catch");
    }

    public void Hurt()
    {
        animator.SetTrigger("hurt");
    }

    public void Death(bool isDead)
    {
        animator.Rebind();
        animator.SetTrigger("isDead");
    }

    public void OnGround(bool onGround)
    {
        animator.SetBool("Grounded", onGround);
    }

    public void OnAirAnimation(Rigidbody target)
    {
        animator.SetFloat("velocityY",target.velocity.y);
    }
}
