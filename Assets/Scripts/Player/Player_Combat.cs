using System;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1f;
    public LayerMask enemyLayers;
    public float damage = 1f;
    
    public Animator anim;
    
    public float cooldownTime = 1f;
    private float cooldownTimer;


    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(cooldownTimer <= 0)
        {
            anim.SetBool("isAttacking", true);
            
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayers);
            
            if(enemies.Length > 0)
            {
                foreach (Collider2D enemy in enemies)
                {
                    enemy.GetComponent<Enemy_Health>().ChangeHealth(-damage);
                }
            }
            
            cooldownTimer = cooldownTime;
        }
    }

    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
