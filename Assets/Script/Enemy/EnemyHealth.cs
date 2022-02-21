using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public HealthBar healthBar;
    //private HUDManager enemyUI;
    private Rigidbody rb;

    public string nome;
    public Sprite image;

    public int maxHealth = 5;
    private int currentHealth;
    private float damageTimer = 0.4f;
    private float deathTimer = 3f;
    
    public bool isDead = false;
    public bool damaged = false;

    void Awake()
    {
        currentHealth = maxHealth;
        //enemyUI = FindObjectOfType<HUDManager>();
        rb = GetComponent<Rigidbody>();
        //healthBar = healthBar.transform.localScale;
    }

    private void Update()
    {

        if (Time.time > damageTimer)
            damaged = false;
    }

    public void TakeDamage(int damage, bool push)
    {
        if (!isDead) 
        {
            damaged = true;
            currentHealth -= damage;
            damageTimer = Time.time + 0.3f;

            if (push)
            {
                rb.AddRelativeForce(new Vector3(7, 10, 0), ForceMode.Impulse);
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
            }
            else
            {
                anim.SetTrigger("hurt");
            }
            
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            //enemyUI.UpdateEnemyUI(nome, maxHealth, currentHealth, image);
        }
    }

    public void Die()
    {
        anim.SetBool("isDead", true);

        deathTimer -= Time.deltaTime;

        if (deathTimer < 0)
            Destroy(this.gameObject);
    }
}
