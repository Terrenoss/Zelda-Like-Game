using UnityEngine;

public class QuestTest : MonoBehaviour
{
    public Quest testQuest; // Assigne la quête que tu veux tester dans l'Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Appuie sur "P" pour progresser dans la quête
        {
            Debug.Log("Ajout de progression à la quête !");
            QuestManager.instance.CompleteTask(testQuest, 1);
        }
    }
}
