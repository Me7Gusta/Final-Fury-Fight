using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyState
    {
        approach,
        prowl,
        attack
    }

    private EnemyState currentState;

    //public Animator anim;
    public Attack attack;
    public Transform groundCheck;
    public Transform target;
    public AudioClip attackSound;
    //public GameObject healthBar;

    private AudioSource audioS;
    private Rigidbody rb;
    private EnemyHealth health;
    private EnemyAnimations anim;

    private float chance;

    public int damage = 1;
    public float dashForce = 10f;
    public float maxSpeed = 5f;
    //public float enemyRange = 1.5f;     //alcance do inimigo
    public float attackRangeX = 2f;    //alcance do ataque do inimigo em X
    public float attackRangeZ = 2f;    //alcance do ataque do inimigo em Z
    public float attackRate = 1f;       //tempo entre ataques do inimigo
    public float minHeight, maxHeght;   //limites do cenário
    
    private float currentSpeed;
    private float hitTime = 0.4f;
    private float nextAttack = 0;       //timer para o inimigo atacar
    private float moveSpeedX = 0;       //velocidade do inimigo na horizontal
    private float hForce = 0;
    private float zForce = 0;           //peso para o inimigo se movimentar na vertical
    private float walkTimer=0;          //timer para o inimigo mudar de direção
    private float walkTime;

    private bool hitting = false;
    private bool onGround = false;
    private bool isDead = false;
    private bool facingRight = false;
    private bool dash = false;
    public LayerMask groundLayers;

    private void Awake()
    {
        //target = FindObjectOfType<Player>().transform;
        Chance();
        target = GameObject.Find("Player").transform;
        health = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<EnemyAnimations>();
        currentSpeed = maxSpeed;
        attack.SetDamage(damage);
        audioS = GetComponent<AudioSource>();
        walkTime = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, groundLayers);
        anim.IsGrounded(onGround);
        isDead = health.isDead;

        if (isDead)
            health.Die();

        if (Time.time > hitTime)
        {
            dash = false;
            hitting = false;
        }

        facingRight = (target.position.x < transform.position.x) ? false : true;
        if (target.position.x < transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        walkTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (onGround)
        {
            States();
        }

    }

    private void States()
    {
        if (!isDead && !health.damaged)
        {
            Vector3 targetDistance = target.position - transform.position;
            Flip();
            switch (currentState)
            {
                case EnemyState.approach:
                    Approach(targetDistance);
                    break;

                case EnemyState.prowl:
                    Prowl();
                    break;

                case EnemyState.attack:
                    AttackPlayer();
                    break;
            }

            float hSpeed, zSpeed;

            if (hitting)
            {
                //currentSpeed = 0;

                hSpeed = 0;
                zSpeed = 0;
                rb.velocity = new Vector3(hSpeed, rb.velocity.y, zSpeed);
            }
            else if (dash)
            {
                //hSpeed = rb.velocity.x;
                zSpeed = rb.velocity.z;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, zSpeed);
            }
            else
            {
                hSpeed = hForce * currentSpeed;
                zSpeed = zForce * currentSpeed;
                rb.velocity = new Vector3(hSpeed, rb.velocity.y, zSpeed);
            }


            Flip();
            anim.WalkAnimation(moveSpeedX, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            anim.WalkAnimation(0, 0);
        }
        float distanceZ = (transform.position - Camera.main.transform.position).z;
        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceZ)).x - 12;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceZ)).x + 12;

        float posX = Mathf.Clamp(rb.position.x, minX, maxX);
        float posZ = Mathf.Clamp(rb.position.z, minHeight, maxHeght);

        rb.position = new Vector3(posX, rb.position.y, posZ);
    }

    private void Approach(Vector3 targetDistance)
    {
        hForce = targetDistance.x / Mathf.Abs(targetDistance.x);
        zForce = targetDistance.z / Mathf.Abs(targetDistance.z);

        if (Mathf.Abs(targetDistance.x) < attackRangeX)
        {
            hForce = 0;
        }

        if (Mathf.Abs(targetDistance.z) < attackRangeZ)
        {
            zForce = 0;
        }

        if ((Mathf.Abs(targetDistance.x) < attackRangeX) && (Mathf.Abs(targetDistance.z) < attackRangeZ)
            && (Time.time > nextAttack))
        {
            currentState = EnemyState.attack;
        }
    }

    private void Prowl()
    {
        if (walkTimer > walkTime)
        {
            walkTime = Random.Range(1f, 2f);
            hForce = Random.Range(-1, 2);
            zForce = Random.Range(-1, 2);
            walkTimer = 0;
            Chance();
        }
    }

    private void AttackPlayer()
    {
        if (dash)
        {
            hitting = false;
        }
        else
        {
            hitting = true;
        }

        anim.Attack();
        PlaySound(attackSound);
        hitTime = Time.time + 0.6f;
        nextAttack = Time.time + attackRate;
        //walkTime = Random.Range(1f, 2f);
        walkTimer = 0;
        Chance();
    }
    public void Dash()
    {
        dash = true;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        if (facingRight)
        {
            rb.AddForce(new Vector3(10, 10, 0), ForceMode.Impulse);
            //Debug.Log("Pulou");
        }
        else
        {
            rb.AddForce(new Vector3(-10, 10, 0), ForceMode.Impulse);
            //Debug.Log("Pulou");
        }
    }

    private void Chance()
    {
        chance = Random.Range(0, 1.0f);
        if (chance < 0.66f)
        {
            currentState = EnemyState.approach;
        }
        else
        {
            currentState = EnemyState.prowl;
        }
    }

    private void Flip()
    {
        moveSpeedX = Mathf.Clamp(rb.velocity.x, -1, 1);

        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            moveSpeedX *= -1;
        }
    }


    public void PlaySound(AudioClip clip)
    {
        audioS.clip = clip;
        if (!hitting)
        {
            audioS.Play();
        }
        //playSoundOnce = false;
    }
}
