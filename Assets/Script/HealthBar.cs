using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;

    private void Start()
    {
        localScale = transform.localScale;
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float percentHealth;

        percentHealth = ((float)currentHealth * 100 / (float)maxHealth) / 100;

        localScale.x = percentHealth;
        transform.localScale = localScale;
    }

   
}
