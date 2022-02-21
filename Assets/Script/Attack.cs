using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //public float attackRange = 0.86f;

    public AudioClip onImpact;

    private AudioSource audioS;
    private int damage = 1;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = onImpact;
    }

    public void SetDamage(int hitDamage)
    {
        damage = hitDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
            
            if (!player.IsDead())
                audioS.Play();
        }
    }

}
