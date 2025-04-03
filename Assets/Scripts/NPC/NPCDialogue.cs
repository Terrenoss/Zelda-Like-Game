using UnityEngine;



// This scriptable object is used to define the dialogue for NPCs in the game.
// It can be used to create a variety of dialogue options and responses for the player to interact with.
[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName; // The name of the NPC
    public string[] dialogueLines;
    public bool[] autoProgressLines; // Array to determine if the line should auto progress
    public float autoProgressDelay = 1.5f; // Time to wait before auto progressing
    public float typingSpeed = 0.05f; // The speed at which the dialogue is displayed
    // public AudioClip voiceSound;
    // public float voicePitch = 1f;
}
