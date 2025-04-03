using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest[] questsToGive; // Permet à un PNJ d'avoir plusieurs quêtes
    private int questIndex = 0;  // Indice de la quête actuelle à donner

    // Vérifie si le PNJ a encore une quête à donner
    public bool HasQuest()
    {
        return questIndex < questsToGive.Length;
    }

    // Récupère la quête actuelle à donner
    public Quest GetCurrentQuest()
    {
        if (HasQuest())
        {
            return questsToGive[questIndex];
        }
        return null;
    }

    // Donne la quête au joueur
    public void GiveQuest()
    {
        if (HasQuest())
        {
            Quest quest = GetCurrentQuest();
            
            // Vérifie si la quête n'est pas déjà dans la liste des quêtes actives
            if (!QuestManager.instance.activeQuests.Contains(quest))
            {
                // Si la quête n'est pas déjà dans les quêtes actives, on l'ajoute
                Debug.Log("Ajout de la quête : " + quest.questName);
                QuestManager.instance.AddQuest(quest);
                
                // Si le PNJ a plus de quêtes à donner, on passe à la suivante
                questIndex++;

                // Affiche un message si toutes les quêtes ont été données
                if (questIndex >= questsToGive.Length)
                {
                    Debug.Log("Toutes les quêtes ont été données !");
                }
            }
            else
            {
                Debug.Log("Le joueur a déjà cette quête !");
            }
        }
        else
        {
            Debug.Log("Aucune quête à donner !");
        }
    }

    // Optionnel : Pour réinitialiser la quête si nécessaire
    public void ResetQuestIndex()
    {
        questIndex = 0;
    }
}