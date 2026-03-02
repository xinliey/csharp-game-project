using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using Unity.VisualScripting;
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
    public SpriteRenderer npc;
    public bool QuestDone;
    [Range(0f, 1f)]
    public float level;   //range of each level       



   private void Awake()
    {   
       
        collider = GetComponent<BoxCollider2D>();
        npc = GetComponent<SpriteRenderer>();
 CheckCurrentRelationship();
    }
    
    public void CheckCurrentRelationship()
    {
        if (GameManager.instance.CheckNextVictimState())//during the next victim state, there will be no npc walking around
        {
            collider.enabled = false;
            npc.enabled = false;
        }
        level = Level;
        if (level >= 1f)
        {

            character.DailyData.TriggerLore = true;
            character.DailyData.questInteract = true;
            CurrentLevel = 0;
            Level = 0f;
            character.DailyData.giftPresent = true;
            //character.DailyData.questInteract = true;
        }
    }

    internal void IncreaseRelationship(float v)
    {
        if(QuestInteract == false){ //if player havent done quest
            //Level += v;
            QuestInteract = true;
        }
        else if(TalkedToToday == false)
        { 
            TalkedOnTheDayNumber += 1;
           // Level += v;
            TalkedToToday = true;
        }
        Level += v;
        
        CheckCurrentRelationship();
    }
    internal void EarlyForSchool(float v) //if player come to school early 
    {
        if (character.DailyData.level != 0f)
        {
           Level += v;
        }
        //if level is 0f mean it's triggering quest hint mode , dont mess it up    

        CheckCurrentRelationship();
    }
    internal void LateForSchoolDeduct(float v)
    {
        Level -= v;
        CheckCurrentRelationship();
    }
    internal void IncreaseRS(float v) //from receiving gift
    {
        if (GiftPresent == false)
        {
            Level += v;
            GiftPresent = true;
        }
        CheckCurrentRelationship();//rechecking status after interacting
        
    }

  
}
