using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CafeDialogue : Interactable
{
    private Character character;
    [SerializeField] DialogueContainer Beginnerdialogue;
    [SerializeField] DialogueContainer Startdialogue;
    [SerializeField] PlayerScoreRecord playerData;
    [SerializeField] BoxCollider2D interaction;
    [SerializeField] SpriteRenderer JayCounter;
    private void Awake()
    {
        character = GetComponent<Character>();
        playerData.inPartTimeScene = true;
        if (playerData.finishedSchool == false)
        {
            JayCounter.enabled = false;
            interaction.enabled = false;
        }
        else
        {
            JayCounter.enabled = true;
            interaction.enabled = true;
        }
    }
    public override void Interact(Character character)
    {
        interaction.enabled = false;
        if (playerData.PartTimeOnDay == 0)
        {
            GameManager.instance.dialogueSystem.Initialize(Beginnerdialogue);
           playerData.PartTimeOnDay += 1;
        }
        
        else
        {
            GameManager.instance.dialogueSystem.Initialize(Startdialogue);
        }
        

    }

}
