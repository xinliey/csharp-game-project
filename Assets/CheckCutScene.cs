using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CheckCutScene : MonoBehaviour
{
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] List<string> Cutscene = new List<string>();
    [SerializeField] List<int> IndexInPlayer = new List<int>();
    public CutSceneDialogue cutscenetoDisplay;
    int Index;

    private void Start()
    {
        if (player.LoadFromMenu == false && player.currentTrigger != null)
        {
            StartCoroutine(LoadingScreen());
        }else if (player.LoadFromMenu== true)
        {
            StartCoroutine(LoadingScreenFromMenu());
        }
    }
    private IEnumerator LoadingScreenFromMenu()
    {
        ScreenFader screenFader = GameManager.instance.screenFader;
        player.LoadFromMenu= false;
        screenFader.imagefade.SetActive(true);
        screenFader.UnTint();
        CheckCutSceneName();
        yield return new WaitForSeconds(3f);
    }
    private IEnumerator LoadingScreen()
    {
        // Wait until the GameManager has been initialized
        yield return new WaitUntil(() => GameManager.instance != null
                                        && GameManager.instance.screenFader != null);

        
        

        CheckCutSceneName();
    }

    public void CheckCutSceneName()
    {
       // Debug.Log("checking cutscene name");
        for(int i = 0; i < Cutscene.Count; i++)
        {
            if (player.currentTrigger == Cutscene[i]) 
            {

                Index = IndexInPlayer[i];
                if (player.quest[Index].isDone == false)
                {
                    //Debug.Log($"this is the index for cutscene:{Index}");
                    cutscenetoDisplay = player.quest[Index].cutscene;
                    GameManager.instance.cutSceneManager.Initialize(cutscenetoDisplay);
                    player.quest[Index].isDone = true;
                }

            }
           
        }
    }
}

