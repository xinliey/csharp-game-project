using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [SerializeField] PlayerScoreRecord data;
    [SerializeField] GameObject exitbtn;
    [SerializeField] GameObject nextday;
    [SerializeField] GameObject backbtn;
    [SerializeField] Vector3 respawnPointPosition = new Vector3(-10, 17, 0);
    DisableControls disableControls;
    Character character;
    GameTime gameTime;
    NPCMove Niki;
    NPCDefintition closestnpc;
    float comparingscore=0; 

    private void Awake()
    {
        //Debug.Log("Sleep script Awake called.");
        //disable player from moving while sleeping
        disableControls = GetComponent<DisableControls>();
        exitbtn.SetActive(false);
        nextday.SetActive(false);
        character = GetComponent<Character>();
        gameTime = GameManager.instance.gameTime;
       

        GameObject npcObj = GameObject.FindGameObjectWithTag("NPC");
        if (npcObj != null)
        {
            Niki = npcObj.GetComponent<NPCMove>();

        }
        else
        {
           // Debug.Log("niki cant be found");
        }
    }
    internal void DoSleep()
    {
        
       
        DisplayBtn();
 
    }
    private void DisplayBtn()
    {
        exitbtn.SetActive(true);
        nextday.SetActive(true);
        backbtn.SetActive(true);
    }
  public void DisablrButton()
    {
        GameManager.instance.ClickButtonSound();
        exitbtn.SetActive(false);
        nextday.SetActive(false);
        backbtn.SetActive(false);
    }
    public void nextDay()
    {
        DisablrButton();
        disableControls.DisableControl();
        StartCoroutine(SleepRoutine());
    }
    public void ExitGame()
    {
        DisablrButton();
        Application.Quit();

    }


    IEnumerator SleepRoutine()
    {
        GameObject npcObj = GameObject.FindGameObjectWithTag("NPC");
        if (npcObj != null)
        {
            Niki = npcObj.GetComponent<NPCMove>();

        }
        else
        {
            //Debug.Log("niki cant be found");
        }
        ScreenFader screenFader = GameManager.instance.screenFader;
        
        screenFader.Tint();
        yield return new WaitForSeconds(2f);
      
        exitbtn.SetActive(false);
        nextday.SetActive(false);
        //SceneTransitionManager.Instance.InitSwitchScene("mc_house", respawnPointPosition);
        character.FullHeal();
        character.FullRest(0);
        gameTime.SkipToMorning(); 
        //character.CheckLevel();
        data.DidParttimeToday = false;
        data.arriveSchool = false;
        data.finishedSchool = false;
        data.TodayTexted = false;
        data.MenuLooked = false;
        data.inPartTimeScene = false;
    
        character.ResetTalkState();
        Niki.ResetSchedule();  
        yield return new WaitForSeconds(3f);
        screenFader.UnTint();
        
        disableControls.EnableControl();
       


        yield return null; 
    }
}
