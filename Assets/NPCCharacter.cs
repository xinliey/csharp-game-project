using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class NPCCharacter : TimeAgent
{
    public NPCDefintition character;
    

    
    public float Level //in float , detail level of the character
    {
        get => character.DailyData.level;
        set => character.DailyData.level = value;
    }

    public int CurrentLevel//for determining the level of the cutscene 
    {
        get => character.DailyData.currentLevel;
        set => character.DailyData.currentLevel = value;
    }

    public bool TalkedToToday
    {
        get => character.DailyData.talkedToToday;
        set => character.DailyData.talkedToToday = value;
    }

    public bool QuestInteract
    {
        get => character.DailyData.questInteract;
        set => character.DailyData.questInteract = value;
    }

    public bool GiftPresent
    {
        get => character.DailyData.giftPresent;
        set => character.DailyData.giftPresent = value;
    }

    public int TalkedOnTheDayNumber
    {
        get => character.DailyData.talkedOnTheDayNumber;
        set => character.DailyData.talkedOnTheDayNumber = value;
    }

    public int LoreLevel
    {
        get => character.DailyData.loreLevel;
        set => character.DailyData.loreLevel = value;
    }
 
    //the level is differiate base on the range number 
    private const float CLASSMATE = 0.19f;
    private const float LORE1 = 0.20f;
    private const float FRIEND = 0.50f;
    private const float LORE2 = 0.50f;
    private const float BESTIES = 0.75f;
    private const float LORE3 = 0.80f;
    private const float LORE4 = 1f;
    private BoxCollider2D collider;
    public bool QuestDone;
    [Range(0f, 1f)]
    public float level;   //range of each level       



   private void Awake()
    {
        
        CheckCurrentRelationship();
        collider = GetComponent<BoxCollider2D>();
    }
    
    public void CheckCurrentRelationship()
    {
        level = Level;
        if (level >= 1f)
        {

            character.DailyData.TriggerLore = true;
            CurrentLevel = 0;
            Level = 0f;
            character.DailyData.questInteract = true;
        }
        /*float roundedLevel = Mathf.Round(Level * 100f) / 100f;
        UnityEngine.Debug.Log($"current score is {roundedLevel}");

        if (roundedLevel < CLASSMATE)
        {
            //  Debug.Log(character.Name + " is a Stranger.");
            CurrentLevel = 1;
        }
        else if (roundedLevel >= LORE1 && character.DailyData.TriggerLore == false) // Show lore1
        {
            UnityEngine.Debug.Log(character.Name + " lore level reach lv1");
            LoreLevel = 0;
            CurrentLevel = 0;
            character.DailyData.TriggerLore = true;

        }
        else if (roundedLevel >= LORE1 && character.DailyData.TriggerLore == false && roundedLevel < FRIEND)
        {
            UnityEngine.Debug.Log(character.Name + " is now a classmate.");
            CurrentLevel = 2;
        }

        else if (roundedLevel >= LORE2 && character.DailyData.TriggerLore == false) // Show lore2
        {
            UnityEngine.Debug.Log(character.Name + " lore level reach lv2.");
            LoreLevel = 1;
            CurrentLevel = 0;
            character.DailyData.TriggerLore = true;
        }
        else if (roundedLevel > LORE2 && character.DailyData.TriggerLore == false && roundedLevel <= BESTIES)
        {
            //Debug.Log(character.Name + " is now a friend.");
            CurrentLevel = 3;
        }
        else if (roundedLevel >= LORE3 && !character.DailyData.TriggerLore==false) // Show lore3
        {
            UnityEngine.Debug.Log(character.Name + " lore level reach lv3.");
            LoreLevel = 2;
            CurrentLevel = 0;
            character.DailyData.TriggerLore = true;
        }
        else if (roundedLevel > LORE3 && character.DailyData.TriggerLore==false&&roundedLevel >= BESTIES)
        {
           //Debug.Log(character.Name + " is now a bestie.");
            CurrentLevel = 4;

        }
        else if (roundedLevel >= LORE4 && character.DailyData.TriggerLore==false) // Show lore4 (final)
        {
            // Debug.Log(character.Name + " lore level reach lv4.");
            UnityEngine.Debug.Log(character.Name + " you have now completed this character's story");
            LoreLevel = 3;
            CurrentLevel = 0;
            character.DailyData.TriggerLore = true;
        }*/
        
    }

    internal void IncreaseRelationship(float v)
    {
        if(QuestInteract == false){ //if player havent done quest
            Level += v;
            QuestInteract = true;
        }
        else if(TalkedToToday == false)
        {  
            Level += v;
            TalkedToToday = true;
        }
        //Level += v;
        TalkedOnTheDayNumber += 1;
        CheckCurrentRelationship();
    }
    internal void LateForSchoolDeduct(float v)
    {
        Level -= v;
        CheckCurrentRelationship();
    }
    internal void IncreaseRS(float v)
    {
        if (GiftPresent == false)
        {
            Level += v;
            GiftPresent = true;
        }
        CheckCurrentRelationship();//rechecking status after interacting
        
    }

    void ResetTalkState(GameTime gameTime)
    {
       
        if(gameTime.isNewDay==true)
        {
            UnityEngine.Debug.Log($"reset talk stage for {character.name}");
            
            TalkedToToday = false;
            TalkedOnTheDayNumber = gameTime.days;
            gameTime.isNewDay = false;
            if (TalkedOnTheDayNumber % 4 == 0) //reset every three days 
            {
                QuestInteract = false;
            }
            if (TalkedOnTheDayNumber%3==0) //reset every three days 
            {
                GiftPresent = false;
            }
            
        }
        
    }
}
