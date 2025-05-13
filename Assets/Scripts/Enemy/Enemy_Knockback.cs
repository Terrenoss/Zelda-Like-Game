using System.Collections;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemyMovement;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }
    
    public void Knockback(Transform playerTransform, float knockbackForce,float knockbackTime, float stunTime)
    {
        enemyMovement.ChangeState(Enemy_Movement.EnemyState.KnockedBack);
        StartCoroutine(Stun(knockbackTime, stunTime));
        Vector2 direction =  (transform.position - playerTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
    }
    
    IEnumerator Stun(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemyMovement.ChangeState(Enemy_Movement.EnemyState.Idle);
    }
}
