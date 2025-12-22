using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [SerializeField] PlayerScoreRecord data;
    [SerializeField] GameObject exitbtn;
    [SerializeField] GameObject nextday;
    [SerializeField] GameObject backbtn;
   
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
            Debug.Log("niki cant be found");
        }
    }
    internal void DoSleep()
    {
        if (data.currentTrigger == "HeeseungConfront")
        {
            foreach (NPCDefintition n in character.npcreset)
            {
                if (n.DailyData.level > comparingscore)
                {
                    closestnpc = n;
                    comparingscore = n.DailyData.level;
                }
            }
            Debug.Log($"the closest npc is{closestnpc.Name}");
            data.closestnpc = closestnpc;
        }
       
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
            Debug.Log("niki cant be found");
        }
        ScreenFader screenFader = GameManager.instance.screenFader;
        
        screenFader.Tint();
        yield return new WaitForSeconds(2f);
      
        exitbtn.SetActive(false);
        nextday.SetActive(false);
        
        character.FullHeal();
        character.FullRest(0);
        gameTime.SkipToMorning(); // this line have problem
        //character.CheckLevel();
        data.DidParttimeToday = false;
        data.arriveSchool = false;
        data.finishedSchool = false;
        data.TodayTexted = false;
        data.MenuLooked = false;
        screenFader.UnTint();
        character.ResetTalkState();
        Niki.ResetSchedule();
        yield return new WaitForSeconds(2f);
        disableControls.EnableControl();
        


        yield return null; 
    }
}
