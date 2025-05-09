using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest[] questsToGive;
    private int questIndex = 0;

    public bool HasQuest()
    {
        // Vérifie d'abord si on a dépassé le tableau
        if (questIndex >= questsToGive.Length || questsToGive[questIndex] == null)
            return false;

        Quest currentQuest = questsToGive[questIndex];

        // Cas 1: Quête non répétable jamais donnée
        if (!currentQuest.isRepeatable && !QuestManager.instance.activeQuests.Contains(currentQuest))
        {
            return true;
        }

        // Cas 2: Quête répétable (terminée ou jamais donnée)
        if (currentQuest.isRepeatable)
        {
            // Si terminée, on peut la redonner
            if (currentQuest.isCompleted)
                return true;
            
            // Si pas encore donnée
            return !QuestManager.instance.activeQuests.Contains(currentQuest);
        }

        return false;
    }

    public Quest GetCurrentQuest()
    {
        if (questIndex < questsToGive.Length)
            return questsToGive[questIndex];
        return null;
    }

    public void GiveQuest()
    {
        if (!HasQuest())
        {
            Debug.Log("Aucune quête disponible à donner actuellement");
            return;
        }

        Quest quest = GetCurrentQuest();
        
        // Réinitialiser si c'est une quête répétable terminée
        if (quest.isRepeatable && quest.isCompleted)
        {
            quest.ResetQuest();
        }

        // Ajouter la quête au manager
        QuestManager.instance.AddQuest(quest);
        Debug.Log($"Quête donnée: {quest.questName} (Répétable: {quest.isRepeatable})");

        // Incrémenter l'index SEULEMENT pour les quêtes non répétables
        if (!quest.isRepeatable)
        {
            questIndex++;
            Debug.Log($"Passage à la quête suivante (index: {questIndex})");
        }
    }

    public void ResetQuestIndex()
    {
        questIndex = 0;
    }
}