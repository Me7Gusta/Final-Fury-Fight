using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm { get; private set; }

    [SerializeField]
    private int maxHealth = 20;
    private int currentHealth;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetMaxHealth();
    }


    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
