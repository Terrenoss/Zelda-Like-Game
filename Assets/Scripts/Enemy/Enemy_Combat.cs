using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    
    public int damage = 30;
    public Transform attackPoint;
    public float weaponRange;
    public float knockBackForce = 2f;
    public float stunTime = 0.5f;
    public LayerMask playerLayer;
    public bool doDamageOnCollision = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(doDamageOnCollision == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.ChangeHealth(-damage);
                }
            }
        }
    }
    
    public void Attack()
    {
        Debug.Log("Attacking!");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockBackForce, stunTime);
        }
    }
}
