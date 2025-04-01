using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image healthSlider;
    public Image easeHealthSlider;

    private float lerpSpeed = 5f;
    private Coroutine damageCoroutine;
    
    void Start()
    {
        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        Initialize(player);
    }
    
    public void Initialize(PlayerHealth playerHealth)
    {
        playerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthPercentage)
    {
        healthSlider.fillAmount = healthPercentage;

        // Si une coroutine est déjà en cours, l'arrêter avant d'en lancer une nouvelle
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    
        damageCoroutine = StartCoroutine(SmoothHealthDecrease());
    }


    private IEnumerator SmoothHealthDecrease()
    {
        yield return new WaitForSeconds(0.2f); // Petit délai Elden Ring

        while (true)
        {
            float current = easeHealthSlider.fillAmount;
            float target = healthSlider.fillAmount;

            if (Mathf.Abs(current - target) < 0.001f)
            {
                easeHealthSlider.fillAmount = target;
                damageCoroutine = null; // Fin de l'effet
                yield break;
            }

            easeHealthSlider.fillAmount = Mathf.Lerp(current, target, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }
}