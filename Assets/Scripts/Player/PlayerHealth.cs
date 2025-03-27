using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int currentHealth;
    public int maxHealth;

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
