using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decideJakePhone : MonoBehaviour
{
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] NPCDefintition jake;
    private void Awake()
    {
        if (player.currentTrigger == "KeepPhone")
        {
            TriggerCutscene();
        }
    }

    public void TriggerCutscene()
    {

        GameManager.instance.cutSceneManager.Initialize(jake.Questcutscene[2].cutscene);
    }
}
