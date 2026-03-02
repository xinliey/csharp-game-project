using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveTimeCheck : MonoBehaviour
{
   // public GameObject player;
    GameTime gametime;
    [SerializeField] PlayerScoreRecord playerData;
    [SerializeField] List<GameObject> npc;
    float late = 10;
    int arrival; 

    private void Awake()
    {
        
        gametime = GameManager.instance.gameTime;
        if (playerData.arriveSchool == false)
        {
            if (gametime.Hours < late)
            {
                SystemMessengerBox.Instance.ShowMessage("Enter classroom and interact with desk(Optional)");
                //return;
                if (playerData.quest[2].isDone == false) //make hs rs grow faster than anyone to trigger first hint
                {
                    AddScoreForHeeseung();
                }//once first hint is triggered , back to normal
            }
            else
            {
                SystemMessengerBox.Instance.ShowMessage("you are late for school , all npc score will be deducted");
                SystemMessengerBox.Instance.ShowMessage("Enter classroom and interact with desk(Optional)");
                DeductScore();
            }
        playerData.arriveSchool = true;
        }
        if (playerData.currentTrigger == "NextVictim")
        {
            GameManager.instance.cutSceneManager.Initialize(playerData.closestnpc.NextVictimDialogue.cutscene);
        }
      

    }
    private void DeductScore()
    {
        foreach (GameObject n in npc)
        {
            NPCCharacter npcScript = n.GetComponent<NPCCharacter>();
            if (npcScript != null)
            {
                npcScript.LateForSchoolDeduct(0.01f); // use 0.2f for float
            }
            else
            {
                Debug.Log("NPCCharacter is null");
            }
        }
    }
    private void AddScoreForHeeseung()
    {
     
            NPCCharacter Heeseung = npc[3].GetComponent<NPCCharacter>();
            Heeseung.EarlyForSchool(0.3f);
        
        
    }
}

