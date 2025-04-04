using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public int targetAmount;
    public int currentAmount;
    public bool isCompleted;
    public bool isRepeatable;

    public void AddProgress(int amount)
    {
        if (!isCompleted)
        {
            currentAmount += amount;
            if (currentAmount >= targetAmount)
            {
                CompleteQuest();
            }
        }
    }

    public void CompleteQuest()
    {
        currentAmount = targetAmount;
        isCompleted = true;
        Debug.Log($"Quête {questName} terminée! (Répétable: {isRepeatable})");
    }

    public void ResetQuest()
    {
        currentAmount = 0;
        isCompleted = false;
        Debug.Log($"Quête {questName} réinitialisée!");
    }
}