using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeeseungConfront : MonoBehaviour
{
    
    [SerializeField] PlayerScoreRecord player;
    NPCDefintition closestnpc;
    private void Awake()
    {
        if (player.currentTrigger == "HeeseungConfront")
        {
            playcutscene();
        }else if(player.currentTrigger == "HeeseungHouse")
        {
            playerHeeseungHouse();
        }
    }
    public void playcutscene()
    {
        StartCoroutine(PlayCutscenesSequentially());
    }
    private void playerHeeseungHouse()
    {
        GameManager.instance.cutSceneManager.Initialize(player.quest[9].cutscene);
        player.quest[9].isDone = true;
    }

    private IEnumerator PlayCutscenesSequentially()
    {
        CutSceneManager cutScene = GameManager.instance.cutSceneManager;

        // Start heeseungconfront cutscene
        cutScene.Initialize(player.quest[10].cutscene);
        player.quest[10].isDone = true;
        // Wait until first cutscene concludes
        yield return new WaitUntil(() => cutScene.concludedialog == true);

        // Now play the next cutscene afterheeseungconfrontNPCname or NPCSave

        closestnpc = player.closestnpc;
        Debug.Log($"loading dialogue for closest npc: {closestnpc.Name}");
        cutScene.Initialize(closestnpc.HeeseungConfrontDialogue.cutscene);
        closestnpc.HeeseungConfrontDialogue.isDone = true;
    }


}
