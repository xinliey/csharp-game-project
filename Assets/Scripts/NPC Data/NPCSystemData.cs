using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NPCSystemData", menuName = "Data/NPCSystemData")]
public class NPCSystemData : ScriptableObject
{
    [System.Serializable]
    public class bla
    {
        public NPCDefintition npc; // Reference to the NPC
        public bool giftedPresent;
        public int talkedTodayNumber;
        public bool talkedToToday;
        public bool questInteract;
    }

   
}
