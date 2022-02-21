using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip onImpact;

    private AudioSource audioS;
    private int _damage = 1;
    private bool _push = false;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = onImpact;
    }

    public void SetDamage(int hitDamage, bool push)
    {
        _damage = hitDamage;
        _push = push;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            if (!enemy.isDead)
                audioS.Play();
            
            enemy.TakeDamage(_damage, _push);
        }
    }
}
