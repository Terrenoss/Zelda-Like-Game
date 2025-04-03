using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questName;   // Nom de la quête
    [TextArea] public string description; // Description détaillée
    public int targetAmount;   // Objectif (ex: tuer 5 monstres)
    public int currentAmount;  // Progression actuelle
    public bool isCompleted;   // Statut de la quête

    
    public void ResetQuest() 
    {
        currentAmount = 0;
        isCompleted = false;
    }

    public void AddProgress(int amount)
    {
        if (!isCompleted)
        {
            currentAmount += amount;
            if (currentAmount >= targetAmount)
            {
                currentAmount = targetAmount;
                isCompleted = true;
                Debug.Log(questName + " terminée !");
            }
        }
    }
}