using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public event Action<float> OnHealthChanged; // Événement pour notifier l'UI

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        float healthPercentage = currentHealth / maxHealth;
        OnHealthChanged?.Invoke(healthPercentage); // Notifie l'UI

        if (currentHealth <= 0)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}