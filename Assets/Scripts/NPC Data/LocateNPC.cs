using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LocateNPC : MonoBehaviour
{
    private string currentScene;
    public NPCDefintition character;
    GameTime gameTime;
    private List<ScheduleEntry> schedule;
    private bool nextScheduled = false;
    int i = 0;
    Rigidbody2D rb2d;
    private bool NPCleaveScene = false;
    [SerializeField] float speed = 3f;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
       // Debug.Log($"current scene is {currentScene}");
        //Debug.Log($"current position is {transform.position}");
        schedule = character.Homeschedule;
        rb2d = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForScene());


        // Example: loop through your schedule to check if NPC should be in this scene now
        /*
        {

            if (nextScheduled == true)
            {
                if (schedule[j].sceneName == scene.name)
                {
                    Debug.Log("enabling npc");

                    if (schedule[j].warp)
                    {
                        transform.position = schedule[j].transform;
                    }
                }
                else
                {
                    Debug.Log("disabling npc");
                }
                break;

            }
        */



    }
    private IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(2f);
        CheckTimeSchedule(GameTime.Instance, i);
        for (int j = 0; j < schedule.Count; j++)
        {
            if (nextScheduled == true)
            {
                //Debug.Log("enabling npc");
                transform.position = schedule[i].transform;
            }
            else
            {
                //Debug.Log("disabling npc");
            }
            break;

        }

    }
    private void FixedUpdate()
    { CheckTimeSchedule(GameTime.Instance, i);
        //playerTransform = GameManager.instance.player.transform.position;
        //Debug.Log(transform.position);
        //DisableNPC();

        if (nextScheduled == true)
        {

            if (schedule[i].inScene == false)
            {
                EnableNPC();
                //Debug.Log($"inscene value is {schedule[i].inScene}");   

            }
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            //Debug.Log("nextschedule is true");
            if (schedule.Count == 0) { return; }  // Ensure we have schedule data
            if (schedule[i].warp == true)
            {
               // Debug.Log("proceed to warping");
                transform.position = schedule[i].transform;

                rb2d.velocity = Vector3.zero;

            }

            Vector3 moveTo = schedule[i].transform;
            if (moveTo == null) { return; }

            /*if (Vector3.Distance(transform.position, playerTransform) < 3f)
            {
                //Debug.Log("interaction range");
                animator.enabled = false;
                return;  // This return prevents animation from being re-enabled
            }*/

            // Move animator.enabled = true outside the interaction range check
            //animator.enabled = true;
            //Debug.Log("out of range");




            if (Vector3.Distance(transform.position, moveTo) < 1f)
            {
                //Debug.Log($"arrived destination for schedule{i}");
                StopMoving();
                if (NPCleaveScene == false)
                {
                    // Debug.Log("npc finish current schedule without leaving the scene");
                    AdvanceSchedule();
                    nextScheduled = false; //not sure about the logic here yet

                    return;
                }
                else if (NPCleaveScene == true)
                {
                    //this game object which this script attached to will be set inactive
                    // Debug.Log($"leave scene value is {NPCleaveScene}");
                    DisableNPC();
                    AdvanceSchedule();
                    nextScheduled = true;
                }
            }
            Vector3 direction = (moveTo - transform.position).normalized;
           // animator.SetFloat("horizontal", direction.x);
          //  animator.SetFloat("vertical", direction.y);

            // Calculate the direction and normalize the result
            direction *= speed;
            rb2d.velocity = direction;
        }
        nextScheduled = false;

    }

    private void AdvanceSchedule()
    {
        //Debug.Log("moving on to next schedule");
        i++; // Move to the next schedule step
        if (i >= schedule.Count)
        {
          //  Debug.Log("Schedule completed. NPC stops moving.");
            nextScheduled = false; // Stop further movement
        }
        else
        {
            nextScheduled = true;
        }
    }
    private void StopMoving()
    {
        rb2d.velocity = Vector3.zero;
    }
        public void EnableNPC()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        
    }
    private void DisableNPC()
    {
        rb2d.velocity = Vector3.zero;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

        private void CheckTimeSchedule(GameTime gameTime , int j)
    {
        if (i >= schedule.Count || i < 0)
        {

            return; // Prevent accessing out of bounds
        }
        string CurrentTime = gameTime.timeText.text;
        string ScheduleTime = schedule[i].time;
        string EndTime = schedule[i].endtime;
        Debug.Log($"current time is {CurrentTime}, schedule time is {ScheduleTime}");
        int current = ConvertTimeToMinutes(CurrentTime);
        int start = ConvertTimeToMinutes(ScheduleTime);
        int end = ConvertTimeToMinutes(EndTime);

        if (start <= current && current <= end)
        {
            Debug.Log("within the time range");     
            nextScheduled = true;
            return; 
        }
    }
    private int ConvertTimeToMinutes(string time)
    {

        string[] splitTime = time.Split(':'); // Splits "08:05" into ["08", "05"]
        int hours = int.Parse(splitTime[0]);
        int minutes = int.Parse(splitTime[1]);

        return (hours * 60) + minutes;
    }
}
