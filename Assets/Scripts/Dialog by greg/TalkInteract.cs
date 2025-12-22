using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TalkInteract : Interactable
{
    public int talkStaminaCost = 1;
    private SpriteRenderer spriteRenderer;
    private Character character;
    private BoxCollider2D collder;
    //[SerializeField] DialogueContainer dialogue;
    NPCCharacter npcCharacter;
    NPCDefintition npcDefintition; //getting the dialogue from the definition
    [SerializeField] private Sprite newSprite; //for gifting animation 
   // [SerializeField] ItemContainer playerInventory;
    ItemPanel toolBarInventory;
    public float score;
    float currentScore;
   
  
    private void Awake()
    {
        character = GetComponent<Character>();
        npcCharacter = GetComponent<NPCCharacter>();
        npcDefintition = npcCharacter.character;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collder = GetComponent<BoxCollider2D>();
       // animator = GetComponent<Animator>();
       
        
    }

    public override void Interact(Character character)
    {
        bool matchedCutscene = false;

        if (character.player.currentTrigger != null)
        {
            collder.enabled = false;
           // Debug.Log("current trigger is not null proceed to check the information");
            matchedCutscene = CheckTriggerName(character);
            if (matchedCutscene==false)
            {
                //Debug.Log("current trigger is not null but does not match proceed to normal");
                if (npcCharacter.QuestInteract == false)
                {
                   // Debug.Log("quest interact proceeding");
                    QuestInteract(character);
                }
                else if (npcCharacter.TalkedToToday == false)
                {
                   // Debug.Log("daily interact proceeding");
                    DailyInteract(character);
                }
            }
        }
        else
        {
            //Debug.Log("current trigger is null proceed to normal");
            
            if (npcCharacter.QuestInteract == false)
            {
               //Debug.Log("proceed to quest interact");
                QuestInteract(character);
            }
            else if (npcCharacter.TalkedToToday == false)
            {
                //Debug.Log("proceed to normal interact");
                DailyInteract(character);
            }
        }

       
    }
    private bool CheckTriggerName(Character character)
    {
        foreach (var cutscene in npcDefintition.Questcutscene)
        {
            if (cutscene.QuestTriggerName == character.player.currentTrigger && cutscene.isDone == false)
            {
                GameManager.instance.cutSceneManager.Initialize(cutscene.cutscene);
                cutscene.isDone = true;

                return true; // found and played a quest cutscene
            }
        }

        return false; // none matched the trigger
    }

    private void DailyInteract(Character character)
    {
       // Debug.Log($"current level is{npcCharacter.CurrentLevel}");
        if (npcCharacter.CurrentLevel == 1)
        {
           
            DialogueContainer dialogueContainer = npcDefintition.generalDialogues[Random.Range
                (0, npcDefintition.generalDialogues.Count)];
            GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
            score = 0.25f;
        }
        else if(npcCharacter.CurrentLevel == 0)
        {
            if (toolBarInventory == null)
            {
                GameObject panelObject = GameObject.Find("ToolBarPanel");
                if (panelObject != null)
                {
                    toolBarInventory = panelObject.GetComponent<ItemPanel>();
                }
                else
                {
                   // Debug.LogWarning("ToolBarPanel not found in the scene!");
                    return; // Skip execution until it is found
                }
            }
            DialogueContainer dialogueContainer = npcDefintition.LoreDialogues[npcCharacter.LoreLevel];
            if (npcDefintition.LoreDialogues[npcCharacter.LoreLevel].givePresent == true)
            {
                //Debug.Log($"npc has given you{npcDefintition.LoreDialogues[npcCharacter.LoreLevel].GiftObject}");
                Item giftObject = npcDefintition.LoreDialogues[npcCharacter.LoreLevel].GiftObject;

                spriteRenderer.sprite = newSprite;
                GameManager.instance.inventoryContainer.Add(giftObject, 1);
                toolBarInventory.Show();
            }
            GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
            // Set talkedToToday to true to ensure player can't immediately level up
            npcCharacter.TalkedToToday = true;
            npcDefintition.DailyData.TriggerLore = false;
            npcDefintition.DailyData.loreLevel += 1; //lore 0 is for lv 1 , lore 1 is for level 2
            npcCharacter.CurrentLevel = npcDefintition.DailyData.loreLevel + 1;
            Debug.Log($"setting trigger lore to {npcDefintition.DailyData.TriggerLore}");
           

        }
        else if (npcCharacter.CurrentLevel == 2) 
        {
            DialogueContainer dialogueContainer = npcDefintition.ClassmateDialogues[Random.Range
                (0, npcDefintition.ClassmateDialogues.Count)];
            score = 0.25f;
            GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
        }
        else if(npcCharacter.CurrentLevel == 3)
        {
            DialogueContainer dialogueContainer = npcDefintition.FriendDialogues[Random.Range
                (0, npcDefintition.FriendDialogues.Count)];
            score = 0.15f;
            GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
        }
        else if(npcCharacter.CurrentLevel == 4)
        {
            DialogueContainer dialogueContainer = npcDefintition.BestieDialogues[Random.Range
                (0, npcDefintition.BestieDialogues.Count)];
            score = 0.10f;
            GameManager.instance.dialogueSystem.Initialize(dialogueContainer);

        }
        //Debug.Log($"score is{score}");
        IncreaseRelationShip(score);
       
    }//add days later , only some day will have quest , create array from questorder to store the date
    private int QuestOrder;
    private void QuestInteract(Character character) //for quest , talkinteract only choose the quest order but the rest of the
        //quest response and outcome will be in dialogue system since response buttons are implanted in the dialogue system
    {   
        QuestOrder = npcCharacter.CurrentLevel;
        npcCharacter.CheckCurrentRelationship();
        character.GetTired(talkStaminaCost);
        
        DialogueContainer dialogueContainer = npcDefintition.QuestDialogues[QuestOrder];
        

        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
       // npcCharacter.TalkedToToday = true;
       npcCharacter.QuestInteract = true;
        


    }
    public void LikeGiftDialogue()
    {
        DialogueContainer dialogueContainer = npcDefintition.LikeGiftDialogues[Random.Range
                (0, npcDefintition.LikeGiftDialogues.Count)];
        IncreaseRelationShip(0.15f);
        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);

    }
    public void HateGiftDialogue()
    {
        DialogueContainer dialogueContainer = npcDefintition.HateGiftDialogues[Random.Range
                (0, npcDefintition.HateGiftDialogues.Count)];
        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
        IncreaseRelationShip(-0.15f);
    }
    public void NormalGiftDialogue()
    {
        DialogueContainer dialogueContainer = npcDefintition.NormalGiftDialogues[Random.Range
               (0, npcDefintition.NormalGiftDialogues.Count)];
        GameManager.instance.dialogueSystem.Initialize(dialogueContainer);
        IncreaseRelationShip(0.10f);
    }
    public void IncreaseRelationShip(float v)
    {
        npcCharacter.IncreaseRelationship(v);
        collder.enabled = true;
   
        
    }
}
