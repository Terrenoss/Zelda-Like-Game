using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Animator anim;
    [SerializeField] private float cooldownTimer;
    
    [Header("Attack Stats")]
    public float attackDamage = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
    
    public float weaponRange = 1f;
    public float knockbackForce = 5f;
    public float knockbackTime = 0.5f;
    public float stunTime = 0.3f;


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
            
            cooldownTimer = attackSpeed;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayers);
        
        if(enemies.Length > 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.isTrigger)
                    continue;
                
                enemy.GetComponent<Enemy_Health>().ChangeHealth(-attackDamage);
                enemy.GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
            }
        }
    }
    
    public void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
