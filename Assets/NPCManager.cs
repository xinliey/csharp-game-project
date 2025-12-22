using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance { get; private set; }//to gain access from other gameobject and scene
    NPCCharacter  currentNPC;
    public List<NPCDefintition> allNPCs = new List<NPCDefintition>();

    private void Awake()
    {
        instance = this;
    }

    public void GetCurrentNPC(NPCCharacter npc)
    {
        if (npc != null)
        {
           this.currentNPC = npc;
            Debug.Log($"current npc is {currentNPC.character.Name}");
        }
       
    }
   public void DeductAllNPC()
    {
        Debug.Log("deducting all npc's scores");
        foreach(NPCDefintition npc in allNPCs)
        {
            npc.DailyData.level -= 0.05f;
        }
    }
    

}
