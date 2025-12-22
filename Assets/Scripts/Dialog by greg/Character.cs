using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    
    public int maxVal;
    public int currVal;
    public Stat(int curr, int max)
    {
        maxVal = max;
        currVal = curr;
    }

    internal void Subtract(int amount)
    {
        //decrease the hp by the pass parameter 
        currVal -= amount; 
    }
    internal void Add(int amount)
    {
        currVal += amount; 
        if(currVal > maxVal)
        {
            currVal = maxVal;
        }
    }

    internal void SetToMax()
    {
        currVal = maxVal;
    }
}

public class Character : MonoBehaviour
{
    public Stat hp;
    public Stat stamina;
    [SerializeField] GameObject messageBoxUI;
    [SerializeField] StatusBar hpBar;
    [SerializeField] StatusBar staminaBar;
    [SerializeField] DisableControls disableControls;
    [SerializeField] PlayerRespawn playerRespawn;
    [SerializeField] public PlayerScoreRecord player;
    public List<NPCDefintition> npcreset = new List<NPCDefintition>();
    CutSceneManager cutsceneManager;
    GameTime gameTime; 
    public bool NewPlayer = false;
    public int expLevel=0;
    public bool isDead;
    public bool isExhausted;
    //private int NextLevel=1;
   
    private void Awake()
    {
   
        cutsceneManager = GameManager.instance.cutSceneManager;
        gameTime = GameManager.instance.gameTime;
        stamina.maxVal = player.maxStamina;
        stamina.currVal = stamina.maxVal;
           
    }
    private void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    
    } 

    public void CheckLevel()
    {
        
        if(player.CutSceneShow == false)
        {
            CutScene(player.currentLevel);
            //Debug.Log($"not new player current level is {player.currentLevel}");
            player.CutSceneShow = true;
        
        }
       

    }

    public void CutScene(int currentCutScene)
    {
        Debug.Log("now in calling cutscene from cutscene script");  
        
        cutsceneManager.GetCutsceneIndex(currentCutScene);
    }
    private void UpdateHPBar()
    {
        hpBar.Set(hp.currVal, hp.maxVal);
    }
    private void UpdateStaminaBar()
    {
        stamina.maxVal = player.maxStamina;
        staminaBar.Set(stamina.currVal, stamina.maxVal);
    }
    public void TakeDamage(int amount)
    {
        hp.Subtract(amount);
        if(hp.currVal <= 0)
        {
            isDead = true;
        }
        UpdateHPBar();
    }
    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHPBar();
    }
    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHPBar();
    }

    public void GetTired(int amount)
    {
        if( isExhausted == true) {return; }
       
        stamina.Subtract(amount);
        if (stamina.currVal <= 0)
        {
            Exhausted();
        }
        UpdateStaminaBar();
    }
    private void Exhausted()
    {

        isExhausted = true;
        Debug.Log("player is exhausted");
        disableControls.DisableControl();
        StartCoroutine(ChangingDay());
       
       
    }
    public void ResetTalkState()
    {
        foreach (NPCDefintition n in npcreset)
        {

            UnityEngine.Debug.Log($"reset talk stage for {n.name}");
            n.DailyData.talkedToToday = false;

            if (n.DailyData.talkedOnTheDayNumber % 4 == 0) //reset every three days 
            {
                n.DailyData.questInteract = false;
            }
            if (n.DailyData.talkedOnTheDayNumber % 3 == 0) //reset every three days 
            {
                n.DailyData.giftPresent = false;
            }

        }
    }
    IEnumerator ChangingDay()
    {
        GameManager.instance.screenFader.Tint();
        playerRespawn.StartRespawn();
        ResetTalkState();
        stamina.SetToMax();
        UpdateStaminaBar();
        yield return new WaitForSeconds(6f);


        //GameManager.instance.screenFader.UnTint();
        GameManager.instance.gameTime.SkipToMorning();
        disableControls.EnableControl();
        isExhausted = false;
        yield return null;
    }
    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }
    public void FullRest(int amount)
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }
 
   
   
}
