using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsHUD : MonoBehaviour
{
    private float lerpTime;
    [SerializeField] private float chipSpeed;
    [SerializeField] private Image frontHB;
    [SerializeField] private Image backHB;


    private void UpdatePlayerHB
    {
        float frontHBFill = frontHB.fillAmount;
        float backHBFill = backHB.fillAmount;
        float HBPercent = currentHealth / maxHealth;
        
    }
}
