using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public class QuestScript
{
    public string trigger;
    public string quest;
}
public class QuestDisplay : MonoBehaviour
{
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] List<QuestScript> questscript;
    [SerializeField] TextMeshProUGUI textbox;
    private void Awake()
    {
        checkcurrentcutscene();
    }
    public void checkcurrentcutscene()
    {
        if (player.currentTrigger == null)
        {
            textbox.text= "interact with classmates for more info";
        }
        else
        {
            foreach(var n in questscript)
            {
                
                if (player.currentTrigger == n.trigger)
                {
                    Debug.Log($"{n.trigger}");
                    Debug.Log($"{n.quest}");
                    textbox.text = n.quest;
                }
              
            }
        }
    }
}
