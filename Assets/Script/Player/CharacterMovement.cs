using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    private AnimationScript animaScript;
    //[SerializeField] private FaceCamSpriteScript charSprite;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float hSpeed = 10f;
    [SerializeField] private float vSpeed = 6f;
    [SerializeField] private float jumpForce = 500f;
    private bool facingRight = true;
    private bool onGround = true;
    private bool startJump = false;
    
    private float minWidth = -10, maxWidth = 10;
    public float minHeight, maxHeight;

    [Range(0 , 1f)]
    [SerializeField]private float movementSmooth = .5f;
    private Vector3 velocity = Vector3.zero;

    public LayerMask groundLayers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animaScript = GetComponent<AnimationScript>();
    }

    private void Update()
    {
        float distanceZ = (transform.position - Camera.main.transform.position).z;
        minWidth = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ)).x;
        maxWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ)).x;
    }

    public void Move(float hMove, float vMove, bool canMove)
    {
        Vector3 targetVelocity;
        float h;
        float v;

        if (canMove)
        {
            h = hMove * hSpeed;
            v = vMove * vSpeed;

            if (hMove > 0 && !facingRight)
            {
                Flip();
                //charSprite.FlipSprite();
            }
            else if (hMove < 0 && facingRight)
            {
                Flip();
                //charSprite.FlipSprite();
            }
        }
        else
        {
            h = 0;
            v = 0;
        }
        
        targetVelocity = new Vector3(h, rb.velocity.y, v);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmooth);

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, minWidth+1, maxWidth-1), 
            rb.position.y, 
            Mathf.Clamp(rb.position.z, minHeight, maxHeight));

        if (onGround)
            animaScript.RunAnimation(hMove, vMove);
        
        animaScript.OnAirAnimation(rb);
        animaScript.OnGround(onGround);
    }

    public bool OnGroundCheck()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, groundLayers);
        return onGround;
    }

    public void StartJump(bool jump)
    {
        if (jump)
        {
            if (onGround)
            {
                startJump = true;
            }
        }
    }

    public void Jump(bool canMove)
    {
        if (startJump && canMove)
        {
            rb.AddForce(0, jumpForce, 0);
            startJump = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        transform.Rotate(0, 180, 0);
    }

    public void SetRbZero()
    {
        rb.velocity = Vector3.zero;
    }
}
