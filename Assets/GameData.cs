using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameSaveData 
{
    public int expLevel; // Save player's current cutscene
    public List<NPCSaveData> npcSaveData = new List<NPCSaveData>(); // Save NPC data
}
[Serializable]
public class NPCSaveData
{
    public string Name;
    public int currentLevel;
    public bool talkedToToday;
    public bool questInteract;
    public bool giftPresent;
    public int talkedOnTheDayNumber;
    public int loreLevel;
}