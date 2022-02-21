using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement movement;
    private CombatScript combat;
    private PlayerHealth health;
    private AnimationScript anim;

    private float horizontalMove;
    private float verticalMove;
    private float timer;
    private float catchTime = 0.183f;

    //private bool isDead = false;
    //private bool pickUp = false;
    private bool isPickingUp = false;
    private bool canMove = true;
    private bool isOnGround;

    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
        combat = GetComponent<CombatScript>();
        health = GetComponent<PlayerHealth>();
        anim = GetComponent<AnimationScript>();
    }

    private void Update()
    {
        isOnGround = movement.OnGroundCheck();
        //isDead = health.isDead;
        
        if (health.Damaged() || combat.IsHitting() || isPickingUp)
            canMove = false;
        else
            canMove = true;
        

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        movement.StartJump(Input.GetButtonDown("Jump"));
        combat.StartCombo(Input.GetButtonDown("Fire1"), Input.GetButtonDown("Fire2"), isOnGround, health.Damaged());
    }

    private void FixedUpdate()
    {
        if (!health.IsDead())
        {
            movement.Move(horizontalMove, verticalMove, canMove);
            movement.Jump(canMove);
            combat.Combo(isOnGround);
            

            if (isPickingUp)
            {
                timer += Time.deltaTime;
                if (timer > catchTime)
                {
                    isPickingUp = false;
                }
            }
        }
        else
        {
            movement.SetRbZero();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealthItem"))
        {
            if (Input.GetButtonDown("Action"))
            {
                if (!other.GetComponent<PickUpItem>().GetPickedUp())
                {
                    int heal;
                    heal = other.GetComponent<PickUpItem>().PickUp();
                    anim.CatchItem();
                    isPickingUp = true;
                    timer = 0;
                    health.Heal(heal);
                    //Destroy(other.gameObject);
                }
            }
        }
    }
}
