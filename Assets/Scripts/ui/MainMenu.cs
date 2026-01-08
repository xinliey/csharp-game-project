using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] string nameEssentialScene;
    [SerializeField] string nameNewGameStartScene;
    [SerializeField] PlayerScoreRecord DataUsing;
    [SerializeField] ItemContainer Inventory;
    [SerializeField] AudioSource Soundtrack;
    
    [SerializeField] AudioSource ClickSound;
    [SerializeField] GameObject NewPlayerPanel;
    [SerializeField] Text Playername;
    [SerializeField] ImageFaderMenu ImageFader;
    [SerializeField] GameObject EndingPanel;
    AsyncOperation operation;

    public void Awake()
    {
        Soundtrack.Play();
    }
    public void ExitGame()
    {
        PlaySoundOfClick();
        Application.Quit();
    }

    public void StartGame()
    {
        
        PlaySoundOfClick();
       
        //Logo.gameObject.SetActive(false);
        NewPlayerPanel.gameObject.SetActive(true);

    }
    
    public void LoadGame()
    {
        PlaySoundOfClick();
        if (DataUsing.ended == false)
        {
            
            ProceedToGameWithData();
        }
        
        
    }

    public void ConfirmNewPlayerName()
    {
        
        if (Playername.text != null || Playername.text == "")
        {
            ResetData();
            DataUsing.currentTrigger = "Intro";
            DataUsing.playerName = Playername.text;
            DataUsing.ended = false;
            ProceedToGameWithData();
        }
        else
        {
            Debug.Log("please enter name");
        }
       
    }
    public void ExitNewPlayerPanel()
    {
        PlaySoundOfClick();
        NewPlayerPanel.gameObject.SetActive(false);
    }
    private void ProceedToGameWithData()
    {
        DataUsing.LoadFromMenu = true;
        Soundtrack.Stop();
        /*SceneManager.LoadScene(nameNewGameStartScene,LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);*/
        ImageFader.Tint(nameNewGameStartScene, nameEssentialScene);
    }
    

    private void ResetData()
    {
        PlaySoundOfClick();
        foreach(ItemSlot slot in Inventory.slots)
        {//cleaning all the inventory 
            Debug.Log("restting inventory");
            slot.Clear();
        }
       
        DataUsing.SaveName = null;
        DataUsing.playerName = null;
        DataUsing.LoadFromMenu = true;
        DataUsing.CutSceneShow = false;
        DataUsing.money = 150;
        DataUsing.maxStamina = 100;
        DataUsing.gameDay = 0;
        DataUsing.currentLevel = 0;
        DataUsing.currentTrigger = null;
        foreach (var npc in DataUsing.npcs)
        {
            if (npc != null)
                npc.ResetDailyData();
            npc.DailyData.level = 0;
            npc.DailyData.loreLevel = 0;
            npc.DailyData.currentLevel = 1;
            npc.DailyData.questInteract = true;
            npc.NextVictimDialogue.isDone = false;
            npc.HeeseungConfrontDialogue.isDone = false;
            npc.HiddenStory.isDone = false;
        }
        DataUsing.finishedSchool = false;
        DataUsing.wordguessScore = 0;
        DataUsing.wordguessReward = 0;
        DataUsing.MessengerTrigger = false;
        DataUsing.TodayTexted = false;
        DataUsing.PartTimeOnDay = 0;
        DataUsing.chloeletterattempt = 0;
        DataUsing.closestnpc = null;
        foreach(var n in DataUsing.quest)
        {
            n.isDone = false;
        }
    }
    private void PlaySoundOfClick()
    {
        ClickSound.Play();
    }

    public void EndingCollectionButton()
    {
        PlaySoundOfClick();
        EndingPanel.SetActive(true);
    }

    public void ClosingEndingTab()
    {
        PlaySoundOfClick();
        EndingPanel.SetActive(false);
    }
}
