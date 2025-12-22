using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenStoryTrigger : Interactable
{
    [SerializeField] NPCDefintition npc;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] BoxCollider2D box;
    public string currentTrigger;

    private void Awake()
    {
        if (npc.HiddenStory.isDone == true)
        {
            //Debug.Log("you've done the hidden story in this map");
            box.enabled = false;
        }
    }
    public override void Interact(Character character)
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        CutSceneManager cutScene = GameManager.instance.cutSceneManager;

        if (npc.HiddenStory.isDone == false)
        {
            currentTrigger = player.currentTrigger;
            GameManager.instance.cutSceneManager.Initialize(npc.HiddenStory.cutscene);
            npc.HiddenStory.isDone = true;
          
            yield return new WaitUntil(() => cutScene.concludedialog == true);  
            player.currentTrigger = currentTrigger;
            box.enabled = false;


        }


    }
}
