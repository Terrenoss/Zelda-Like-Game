using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    
    public int damage = 1;
    
    private void OnCollisionEnter2D(Collision2D collision)
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
