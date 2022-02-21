using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    [SerializeField] private Animator anima;
    [SerializeField] private PlayerAttack attackpoint;
    [SerializeField] private AudioSource audioS;
    //public Transform groundCheck;

    public Combo normalAttackCombo;
    public Hit strongAttack;
    public Hit airAttack;
    public List<string> currentCombo;
    private Hit currentHit, nextHit;

    //[SerializeField] private float attackRange = .5f;
    private float comboTimer;

    private bool canHit = true;
    private bool startCombo = false;
    private bool startStrongAttack = false;
    private bool startAirAttack = false;
    private bool playSoundOnce = false;
    
    public LayerMask enemyLayers;

    public bool IsHitting()
    {
        if (startCombo || startStrongAttack)
            return true;
        else
            return false;
    }

    public void StartCombo(bool normalAttack, bool strongAttack, bool onGround, bool beingDamaged)
    {
        if (!beingDamaged)
        {
            if (normalAttack)
            {
                if (onGround)
                {
                    if (normalAttackCombo.hits.Length > currentCombo.Count)
                        if (canHit)
                        {
                            startCombo = true;
                            nextHit = normalAttackCombo.hits[currentCombo.Count];
                            canHit = false;
                            playSoundOnce = true;
                        }
                }
                else if (!onGround)
                    if (canHit)
                    {
                        //attackpoint.SetDamage(airAttack.damage, airAttack.push);
                        anima.SetTrigger("airAttack");
                        startAirAttack = true;
                        playSoundOnce = true;
                        canHit = false;
                    }
            }
            else if (strongAttack && onGround && !startCombo)
            {
                if (canHit)
                {
                    comboTimer = 0f;
                    startStrongAttack = true;
                    playSoundOnce = true;
                    canHit = false;
                }
            }
        }
    }

    public void Combo(bool onGround)
    {
        if (startCombo)
        {
            if (currentCombo.Count == 0)
                PlayHit(normalAttackCombo.hits[currentCombo.Count]);

            comboTimer += Time.deltaTime;

            if (comboTimer >= currentHit.animationTime && !canHit)
            {
                PlayHit(nextHit);
            }

            if (comboTimer >= currentHit.resetTime)
            {
                ResetCombo();
            }
        }
        else if(startStrongAttack)
        {
            attackpoint.SetDamage(strongAttack.damage, strongAttack.push);
            PlaySound(strongAttack.hitSound);
            anima.Play(strongAttack.animation);
            comboTimer += Time.deltaTime;
            
            if (comboTimer >= strongAttack.resetTime)
            {
                canHit = true;
                ResetCombo();
            }
        }
        else if (startAirAttack)
        {
            attackpoint.SetDamage(airAttack.damage, airAttack.push);
            PlaySound(airAttack.hitSound);
            //comboTimer += Time.deltaTime;

            if (/*comboTimer >= airAttack.resetTime &&*/ onGround)
            {
                startAirAttack = false;
                canHit = true;
            }
        }
    }

    private void PlayHit(Hit hit)
    {
        attackpoint.SetDamage(hit.damage, hit.push);
        PlaySound(hit.hitSound);
        anima.Play(hit.animation);
        //Attack(hit.damage);

        currentCombo.Add(hit.animation);
        currentHit = hit;
        comboTimer = 0f;
        canHit = true;
    }

    private void ResetCombo()
    {
        startCombo = false;
        startStrongAttack = false;
        comboTimer = 0f;
        currentCombo.Clear();
        anima.Rebind();
        canHit = true;
    }

    private void PlaySound(AudioClip clip)
    {
        audioS.clip = clip;
        if (playSoundOnce)
        {
            audioS.Play();
        }
        playSoundOnce = false;
    }
    /*
    private void Attack(int damage)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackpoint.transform.position, attackRange, enemyLayers);


        foreach (CapsuleCollider enemy in hitEnemies)
        {
            //float enemyPosY = enemy.GetComponent<Enemy>().groundCheck.position.y;

            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;

        Gizmos.DrawWireSphere(attackpoint.transform.position, attackRange);
    }*/
}
