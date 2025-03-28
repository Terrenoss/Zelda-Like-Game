using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public float currentHealth;
    public float maxHealth;
    
    private float lerpSpeed = 0.5f;
    public Image healthSlider;
    public Image easeHealthSlider;

    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // if (healthSlider.fillAmount != currentHealth/maxHealth)
        // {
        //     healthSlider.fillAmount = currentHealth/maxHealth;
        // }
        //
        // if(healthSlider.fillAmount != easeHealthSlider.fillAmount)
        // {
        //     easeHealthSlider.fillAmount = Mathf.Lerp(easeHealthSlider.fillAmount, currentHealth/maxHealth, lerpSpeed);
        // }
    }

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
        
        UpdatePlayerHB();
    }
    
    
    public void UpdatePlayerHB()
    {
        if(healthSlider.fillAmount != currentHealth/maxHealth)
        {
            healthSlider.fillAmount = currentHealth/maxHealth;
        }
        
        if(healthSlider.fillAmount != easeHealthSlider.fillAmount)
        {
            easeHealthSlider.fillAmount = Mathf.Lerp(easeHealthSlider.fillAmount, healthSlider.fillAmount, lerpSpeed);
        }
    }
}
