using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

//this scriptable is defining the detail of each npc

public enum Gender{
    Male,
    Female
}

[Serializable]
public class PortraitsCollection
{
    public Texture2D normal;
    public Texture2D happy;
    public Texture2D angry;
}
[Serializable]
public class NPCInteraction
{
    public float level;
    public int currentLevel = 1;
    public bool talkedToToday;
    public bool questInteract = true;
    public bool giftPresent;
    public int talkedOnTheDayNumber = 0;
    public int loreLevel = 0;
    public bool TriggerLore=false;
}
[Serializable]
 public class QuestCutScene
{
    public String QuestTriggerName;
    public bool isDone;
    public CutSceneDialogue cutscene;
  
}

[Serializable]
public class ScheduleEntry
{
    public string note;
    public Vector3 transform; // Acts as a destination
    public string time; // Scheduled time to go (format: "HH:mm")
    public string endtime;
    public bool leaveScene;
    public bool inScene;
    public bool warp;
    public string sceneName;
    
}



[CreateAssetMenu(menuName = "Data/NPC Character")]
public class NPCDefintition : ScriptableObject
{
    public void ResetDailyData()
    {
        DailyData.level = 0;
        DailyData.currentLevel = 0;
        DailyData.talkedToToday = false;
        DailyData.questInteract = true;
        DailyData.giftPresent = false;
        DailyData.talkedOnTheDayNumber = 0;
        DailyData.loreLevel = 0;
    }
    public string Name = "Nameless";
    public Gender gender = Gender.Male;

    public PortraitsCollection portraits;
    public NPCInteraction DailyData;
    //crreate prefab of the character to be used on the scene 
    public GameObject characterPrefab;


    [Header("Interaction")] //create the header to seperate them
    public List<Item> itemLike;
    public List<Item> itemHate;

    [Header("Quest Trigger")]
    public List<QuestCutScene> Questcutscene = new List<QuestCutScene>();
    public QuestCutScene NextVictimDialogue;
    public QuestCutScene HeeseungConfrontDialogue;
    public QuestCutScene HiddenStory; 

    [Header("Schedule")]
    public List<ScheduleEntry> Homeschedule = new List<ScheduleEntry>();
    public List<ScheduleEntry> Townschedule = new List<ScheduleEntry>();
    public List<ScheduleEntry> Schoolschedule = new List<ScheduleEntry>();
    [Header("Lv1 Dialogues")]
    public List<DialogueContainer> generalDialogues;

    [Header("Lv2 Dialogues")]
    public List<DialogueContainer> ClassmateDialogues;

    [Header("Lv3 Dialogues")]
    public List<DialogueContainer> FriendDialogues;
    [Header("Lv4 Dialogues : best friend")]
    public List<DialogueContainer> BestieDialogues; 
    [Header("lore Dialogues")]
    public List<DialogueContainer> LoreDialogues;
    [Header("Quesg Dialogue")]
    public List<DialogueContainer> QuestDialogues;
    [Header("Like Gift Dialogue")]
    public List<DialogueContainer> LikeGiftDialogues;
    [Header("Hate Gift Dialogue")]
    public List<DialogueContainer> HateGiftDialogues;
    [Header("Normal Gift Dialogue")]
    public List<DialogueContainer> NormalGiftDialogues;

}
