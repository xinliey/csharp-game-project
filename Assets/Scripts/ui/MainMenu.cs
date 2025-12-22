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

    
    [SerializeField] AudioSource ClickSound;
    [SerializeField] GameObject NewPlayerPanel;
    [SerializeField] Text Playername;
    [SerializeField] ImageFaderMenu ImageFader;


    AsyncOperation operation;
    
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
        ProceedToGameWithData();
        
    }

    public void ConfirmNewPlayerName()
    {
        
        if (Playername.text != null || Playername.text == "")
        {
            ResetData();
            DataUsing.currentTrigger = "Intro";
            DataUsing.playerName = Playername.text;
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
        /*SceneManager.LoadScene(nameNewGameStartScene,LoadSceneMode.Single);
        SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);*/
        ImageFader.Tint(nameNewGameStartScene, nameEssentialScene);
    }
    

    private void ResetData()
    {
        foreach(ItemSlot slot in Inventory.slots)
        {//cleaning all the inventory 
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
}
