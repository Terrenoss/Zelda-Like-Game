using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float damage)
    {
        currentHealth += damage;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
