using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //private Rigidbody rb;
    public GameObject gameOverScreen;
    public MusicManager music;

    public AudioClip healingSound;

    private AudioSource audioS;
    private AnimationScript anim;
    private HUDManager healthbar;

    private int maxHealth = 10;
    private int currentHealth;

    private float hitTimer = 0;

    private float deathTime = 1f;
    private float timer;

    private bool isDead = false;
    private bool playOnceDeathAnim = true;
    private bool playOnceGameOver = true;
    private bool damaged = false;

    private void Awake()
    {
        maxHealth = GameManager.gm.GetMaxHealth();
        currentHealth = GameManager.gm.GetCurrentHealth();
        audioS = GetComponent<AudioSource>();
        anim = GetComponent<AnimationScript>();
        healthbar = FindObjectOfType<HUDManager>();
        healthbar.SetMaxHealth(maxHealth);
    }

    private void FixedUpdate()
    {
        if (Time.time > hitTimer)
            damaged = false;
           
        healthbar.SetHealth(currentHealth);

        if (isDead)
        {
            
            if (playOnceDeathAnim)
            {
                anim.Death(isDead);

                playOnceDeathAnim = false;
            }
            
            timer += Time.deltaTime;
            if (timer >= deathTime)
            {
                if (playOnceGameOver)
                {
                    GameOverScreen();
                    playOnceGameOver = false;
                }
                //SceneManager.LoadScene("Menu");
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            damaged = true;
            currentHealth -= damage;
            GameManager.gm.SetHealth(currentHealth);
            hitTimer = Time.time + 0.3f;

            if (currentHealth <= 0)
            {
                isDead = true;
                //rb.AddRelativeForce(new Vector3(7, 7, 0), ForceMode.Impulse);
            }
            else { 
                anim.Hurt();
            }
        }
    }

    public void Heal(int heal)
    {
        audioS.clip = healingSound;
        audioS.Play();
        currentHealth = currentHealth + heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        GameManager.gm.SetHealth(currentHealth);
    }

    public bool Damaged()
    {
        return damaged;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void GameOverScreen()
    {
        Time.timeScale = 0f;
        music.PlaySong(music.gameOverSong);
        gameOverScreen.SetActive(true);
    }
}
