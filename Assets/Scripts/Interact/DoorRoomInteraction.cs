using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoomInteraction : Interactable
{
    public string targetSceneName;      // Name of the target scene
    public Vector3 targetSpawnPosition; // Position where the player will spawn in the new scene
    public string npcName;    // Name of the NPC
    public int requiredAffectionScore = 10; // Required affection score to interact with the door

    private void Start()
    {
        // Optional: Initialize any required variables or settings
    }

    public void Interact()
    {
        int affectionScore = GetAffectionScoreWithNPC(npcName); // Get the affection score with npc1

        // Check if the affection score is null or below the required threshold
        if (affectionScore < requiredAffectionScore || affectionScore == 0)
        {
            // If the player is not friends with npc1 (score too low or null), show message
            ShowDialogue("You are not friends with " + npcName + ".");
        }
        else
        {
            // If the affection score is high enough, allow interaction
            Debug.Log("Door interaction triggered!");
            //SceneTransitionManager.Instance.TransitionToScene(targetSceneName, targetSpawnPosition);
        }
    }

    private int GetAffectionScoreWithNPC(string npcName)
    {
        // This function should fetch the player's affection score with the specified NPC.
        // For now, it will return a dummy value. Replace with your actual logic to get the affection score.

        // Example of getting affection score (You should replace this with actual database or save system logic)
        int affectionScore = PlayerPrefs.GetInt(npcName + "_affection", 0); // Default to 0 if no score exists
        return affectionScore;
    }

    private void ShowDialogue(string message)
    {
        // This method will display the message in a dialogue box (replace with your dialogue system)
        Debug.Log(message); // Placeholder for dialogue system
        // Example: DialogueManager.Instance.ShowDialogue(message);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When the player enters the trigger zone, enable interaction (handled by PlayerController)
            Debug.Log("Player entered interaction zone for door: " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // When the player exits the trigger zone, disable interaction
            Debug.Log("Player exited interaction zone for door: " + gameObject.name);
        }
    }
}
