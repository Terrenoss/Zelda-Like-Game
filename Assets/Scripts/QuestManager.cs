using System.Collections;
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

    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            // Réinitialiser l'alpha avant d'ajouter la quête
            CanvasGroup canvasGroup = questPanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }

            quest.ResetQuest();
            activeQuests.Add(quest);
            UpdateUI(quest);
        
            // Forcer l'activation et la visibilité du panel
            questPanel.SetActive(true);
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }
        }
    }

    public void UpdateUI(Quest quest)
    {
        if (activeQuests.Count > 0)
        {
            // S'assurer que le panel est visible
            QuestUIFadeOut fadeScript = questPanel.GetComponent<QuestUIFadeOut>();
            if (fadeScript != null)
            {
                fadeScript.ResetAlpha();
            }
            else
            {
                CanvasGroup cg = questPanel.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    cg.alpha = 1f;
                }
            }

            questPanel.SetActive(true);
            nameText.SetText(quest.questName);
            questText.SetText(quest.description + " (" + quest.currentAmount + "/" + quest.targetAmount + ")");
        }
    }

    public void CompleteTask(Quest quest, int amount)
    {
        if (activeQuests.Contains(quest))
        {
            quest.AddProgress(amount);
            Debug.Log("Progression mise à jour pour " + quest.questName + " : " + quest.currentAmount + "/" + quest.targetAmount);
            UpdateUI(quest);

            if (quest.isCompleted)
            {
                questText.SetText("Quête terminée !");
                Debug.Log("Quête terminée : " + quest.questName);

                if (quest.isRepeatable)
                {
                    RemoveQuest(quest); // Retirer la quête de l'UI
                    quest.ResetQuest(); // Réinitialiser la quête pour pouvoir la reprendre plus tard
                    // Pas de suppression, juste réinitialisation
                }
                else
                {
                    RemoveQuest(quest); // Supprimer définitivement si non répétable
                }
            }
        }
        else
        {
            Debug.LogWarning("La quête " + quest.questName + " n'est pas dans activeQuests !");
        }
    }




    public void RemoveQuest(Quest quest)
    {
        Quest questToRemove = activeQuests.Find(q => q.questName == quest.questName);

        if (questToRemove != null)
        {
            activeQuests.Remove(questToRemove);
        
            // Démarrer le fondu
            QuestUIFadeOut fadeScript = questPanel.GetComponent<QuestUIFadeOut>();
            if (fadeScript == null)
            {
                fadeScript = questPanel.AddComponent<QuestUIFadeOut>();
            }
            fadeScript.StartFadeOut(1.5f);

            // Mettre à jour l'UI après le fondu
            StartCoroutine(UpdateUIAfterFade(1.5f));
        }
    }

    private IEnumerator UpdateUIAfterFade(float delay)
    {
        yield return new WaitForSeconds(delay);
    
        if (activeQuests.Count > 0)
        {
            // Réinitialiser l'alpha avant d'afficher la prochaine quête
            CanvasGroup canvasGroup = questPanel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }
        
            UpdateUI(activeQuests[0]);
            questPanel.SetActive(true);
        }
        else
        {
            questPanel.SetActive(false);
        }
    }
}
