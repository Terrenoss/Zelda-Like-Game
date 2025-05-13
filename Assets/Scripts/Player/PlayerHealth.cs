using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float> OnHealthChanged; // Événement pour notifier l'UI
    
    [Header("Health Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool canRegen = true;
    
    public float healthRegen = 1f;
    public float healthRegenCooldown = 5f;
    public float healthRegenAmount = 5f;
    public float healthRegenDuration = 2f;
    public float healthRegenCooldownTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(HealthRegen());
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

    IEnumerator HealthRegen()
    {
        while (true)
        {
            if (canRegen && currentHealth < maxHealth)
            {
                if (healthRegenCooldownTimer <= 0)
                {
                    currentHealth += healthRegenAmount;
                    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

                    float healthPercentage = currentHealth / maxHealth;
                    OnHealthChanged?.Invoke(healthPercentage); // Notifie l'UI

                    yield return new WaitForSeconds(healthRegenDuration);
                }
                else
                {
                    healthRegenCooldownTimer -= Time.deltaTime;
                }
            }
            yield return null;
        }
    }
}