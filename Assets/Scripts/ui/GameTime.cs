using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.TextCore.Text;

public class GameTime : MonoBehaviour
{
    const float secondInDay = 86400f;
    const float phaseLength = 600f; // checking logic every 10 seconds
    const float phasesInDay = 96f;
    public static GameTime Instance { get; private set; }
   
    float time; 
    [SerializeField] float timeScale = 60f; // 1real second = 60 in-game minute
    [SerializeField] float startAtTime = 28800f;
    //set up the morning time
    [SerializeField] float morningTime = 28800f; 
    public Text timeText;
    public Text dateText; // Reference to the UI Text for date display
    bool respawn = false;
    public Image screenOverlay;
    [SerializeField] PlayerScoreRecord player;
    private int currentDay;
    Vector3 respawnPointPosition = new Vector3(-10, 17, 0);
    public bool isNewDay = false;
    List<TimeAgent> agents;
    public int days;
    public bool isPaused = false;
    void Awake()
    {
        screenOverlay.gameObject.SetActive(false);
        //currentDay = player.gameDay;
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
    }

    // Inform the TimeAgent when to do some action
    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    } 
    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }
    public float Hours
    {
        get { return time / 3600f; }
    }
    public float Minutes
    {
        get { return time % 3600f / 60f; }
    }

   
    private void Update()
    {
        if(isPaused == false)
        {
            
            time += Time.deltaTime * timeScale;
        
            TimeValueCalculation();
            
            if (time > secondInDay)
            {
            SkipToMorning();
            }
        TimeAgent();

        }
        else
        {
            //Debug.Log("pausing time");
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            SkipTime(hours: 1);
        }
    }
    public void SkipSchoolTime()
    {
        // Debug.Log("skipping time");

        SkipTime(hours:6);
        SystemMessengerBox.Instance.ShowMessage("school is finished");
    }
    private void TimeValueCalculation()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
        dateText.text = GetCurrentDate(); 
        UpdateDayNightCycle(Hours);
        if (respawn == false && timeText.text == "20:00")
        {
            respawn = true;

           // for player who havent go to bed by 8pm
            SceneTransitionManager.Instance.Respawn(respawnPointPosition, "mc_house");
            SkipToMorning();

        }
       
    }
   
    int oldPhase = -1;
    private void TimeAgent()
    {
        if(oldPhase == -1)
        {
            oldPhase = CalculatePhase();
        }
        int currentPhase = CalculatePhase();
        while(oldPhase < currentPhase)
        {
            oldPhase += 1;
            for(int i=0; i< agents.Count; i++)
            {
                agents[i].Invoke(this); 
            }
        }
     
    }
    private int CalculatePhase()
    {
        return (int)(time / phaseLength) + (int)(days * phasesInDay); ;
    }
    public void SkipTime(float seconds = 0 , float minute = 0 , float hours = 0)
    {
     //   Debug.Log("inside skiptime in gametime");
        float timeToSkip = seconds;
        timeToSkip += minute * 60f;
        timeToSkip += hours * 3600f;
        time += timeToSkip;
    }

    public void SkipToMorning()
    {
        //Debug.Log("advancing the day");
        if (time >= secondInDay)
        {
                time -= secondInDay; // Reset time to within a single day's range     
        }
        //currentDay = currentDay +1;
        days += 1;
        time = morningTime; // Set time to morning
        player.gameDay += 1;
        dateText.text = GetCurrentDate();

        player.MenuLooked = false;
        player.DidParttimeToday = false ;
        player.arriveSchool = false;
        player.finishedSchool = false;
        player.TodayTexted = false;
        player.FirstOrder = true;
        isNewDay = true;
    }

    public string GetCurrentTime()
    {
        int hh = (int)Hours;   // Cast to int for formatting
        int mm = (int)Minutes; // Cast to int for formatting
        return $"{hh:D2}:{mm:D2}";
    }

    public string GetCurrentDate()
    {
        return $"Day {player.gameDay}";
    }
 
    void UpdateDayNightCycle(float currentHour)
    {
        if (currentHour >19) // Daytime (6 AM to 6 PM)
        {
            if (player.inPartTimeScene == false)
            {
                screenOverlay.gameObject.SetActive(true);
            }

        }
        else
        {
            screenOverlay.gameObject.SetActive(false);
        }
    }
    public void PauseTime()
    {
        isPaused = true; 
    }
    public void ResumeTime()
    {
        isPaused = false;
    }
}
