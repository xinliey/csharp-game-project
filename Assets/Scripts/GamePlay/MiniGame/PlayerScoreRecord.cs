using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine.UI;

[Serializable]
public class AllQuests
{
    public String QuestTriggerName;
    public bool isDone;
    //public bool isEnding;
    public CutSceneDialogue cutscene;
    
}



[CreateAssetMenu(menuName = "Data/Player")]
public class PlayerScoreRecord : ScriptableObject
{

    public string SaveName;
    public string playerName;
    public bool LoadFromMenu;
    public int gameDay;
    public int maxStamina;
    public int money;
    public int currentLevel;
    public string currentTrigger;
    public List<NPCDefintition> npcs = new List<NPCDefintition>();
    public bool CutSceneShow = false;
    public List<AllQuests> quest = new List<AllQuests>();
    public bool ended;
    public List<CutSceneDialogue> ending = new List<CutSceneDialogue>();
    public int wordguessScore;
    public int wordguessReward;
    public bool arriveSchool = false;
    public bool InMiniGameScene = false;
    public bool finishedSchool = false; //reset everyday 
    public bool MessengerTrigger = false;
    public int MessengeIndex;
    public bool TodayTexted;
    public int PartTimeOnDay = 0;
    public bool MenuLooked = false;
    public string currentDessert;
    public string currentCooking; 
    public string dessertHolding;
    public bool ordered = false;
    public bool FirstOrder=false;
    public bool correct = false;
    public bool isDessertInHand = true;
    public bool DidParttimeToday = false;
    public bool inPartTimeScene = false;
    public bool chloeletterInHand;
    public int chloeletterattempt;
    public NPCDefintition closestnpc;
 
    
}
