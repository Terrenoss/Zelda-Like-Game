using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public GameObject questPanel;
    public TMP_Text questText, nameText;

    public List<Quest> activeQuests = new List<Quest>(); // Liste des quêtes en cours

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        questPanel.SetActive(false); // On cache le panel au début
    }

    // Ajouter une quête à la liste active
    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            quest.ResetQuest(); // Réinitialiser si la quête a déjà été faite
            activeQuests.Add(quest);
            UpdateUI(quest);
        }
    }

    // Mettre à jour l'affichage de la quête
    public void UpdateUI(Quest quest)
    {
        questPanel.SetActive(true); // On affiche le panel de la quête
        nameText.SetText(quest.questName); // On met à jour le nom de la quête
        questText.SetText(quest.description + " (" + quest.currentAmount + "/" + quest.targetAmount + ")"); // On affiche la description et la progression
    }

    // Mettre à jour la progression de la quête
    public void CompleteTask(Quest quest, int amount)
    {
        if (activeQuests.Contains(quest))
        {
            quest.AddProgress(amount); // On ajoute la progression de la quête
            UpdateUI(quest); // On met à jour l'UI
            if (quest.isCompleted)
            {
                questText.SetText("Quête terminée !"); // Si la quête est terminée, on affiche "Quête terminée"
            }
        }
    }

    // Optionnel : Retirer une quête terminée de la liste active
    public void RemoveQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            Debug.Log("La quête " + quest.questName + " a été terminée et retirée.");
        }
    }
}
