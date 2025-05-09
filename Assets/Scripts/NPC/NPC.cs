using UnityEngine;
using TMPro;
using System.Collections;

public class NPC : MonoBehaviour
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public GameObject InteractIcon;
    public QuestGiver questGiver; // Peut être NULL
    public bool playerNearby = false;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerNearby)
        {
            Interact();
        }
    }

// Seule la partie Interact() a été modifiée
    public void Interact()
    {
        Debug.Log("Interaction avec le PNJ déclenchée !");

        if (isDialogueActive)
        {
            NextLines();
        }
        else
        {
            StartDialogue();
        
            if (questGiver != null)
            {
                Quest currentQuest = questGiver.GetCurrentQuest();
            
                if (currentQuest != null)
                {
                    Debug.Log($"Quête actuelle: {currentQuest.questName}");
                    Debug.Log($"- Répétable: {currentQuest.isRepeatable}");
                    Debug.Log($"- Terminée: {currentQuest.isCompleted}");
                    Debug.Log($"- Active: {QuestManager.instance.activeQuests.Contains(currentQuest)}");
                }

                if (questGiver.HasQuest())
                {
                    Debug.Log("Donner la quête...");
                    questGiver.GiveQuest();
                }
                else
                {
                    Debug.Log("Le PNJ n'a pas de quête à donner actuellement");
                }
            }
        }
    }

    public void NextLines()
    {
        if (isTyping)
        {
            // Si une ligne est en train de s'écrire, on l'affiche complètement
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // Si on a encore des lignes de dialogue, on lance la suivante
            StartCoroutine(TypeLine());
        }
        else
        {
            // Si c'est la fin du dialogue, on le termine
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        // Démarre le dialogue
        isDialogueActive = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Affiche l'icône d'interaction si le joueur entre dans la zone de déclenchement
            InteractIcon.SetActive(true);
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Cache l'icône d'interaction si le joueur sort de la zone
            InteractIcon.SetActive(false);
            playerNearby = false;
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        
        // Tape chaque caractère un par un
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }
        isTyping = false;

        // Si le dialogue doit progresser automatiquement, on passe à la ligne suivante après un délai
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLines();
        }
    }

    public void EndDialogue()
    {
        // Terminer le dialogue
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
    }
}
