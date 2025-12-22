using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//ensure component this thing can only exist with the object with the rigidbody requirement
[RequireComponent(typeof(Rigidbody2D))]
public class NPCMove : TimeAgent
{
    public NPCDefintition character;
    Rigidbody2D rb2d;
    private Vector3 playerTransform;
    private string ScheduleTime;
    private string EndTime;
    private string CurrentTime;
    [SerializeField] float speed = 3f;
    [SerializeField] private Sprite newSprite;
    Animator animator;
    private bool nextSchedule = false;
    private bool NPCleaveScene = false;
    //private bool NPCinScene = false;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    int i=0;
    public bool HomeSchedule;
    public bool TownSchedule;
    public bool SchoolSchedule;
    public Vector3 initialPosition;
    private List<ScheduleEntry> schedule;
    private string currentScene;


    private void Awake()
    {
        
        currentScene = SceneManager.GetActiveScene().name;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get SpriteRenderer
        boxCollider = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;

       // Debug.Log($"current position is {transform.position}");

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
    }

    private IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(2f);
        
        for (int j = 0; j < schedule.Count; j++)
        {
            CheckTimeSchedule(GameTime.Instance, j);
            //Debug.Log($"coming back from check time, next schedule is {nextSchedule}");
            if (nextSchedule == true)
            {
                EnableNPC();    
                //Debug.Log($"enabling npc for schedule {j}, transform :{schedule[j].transform}");
                transform.position = schedule[j].transform;
                i = j;
                break;
            }
            else
            {
               // Debug.Log("disabling npc");
            }
            

        }

    }
    private void UpdateActiveSchedule()
    {
        if (HomeSchedule == true)
        {
            schedule = character.Homeschedule;
        }
        else if (TownSchedule == true)
        {
            schedule = character.Townschedule;
        }
        else if (SchoolSchedule == true)
        {
            schedule = character.Schoolschedule; // Default to town schedule
        }
    }

    private void FixedUpdate()
    {
        UpdateActiveSchedule();
        CheckTimeSchedule(GameTime.Instance, i);
        playerTransform = GameManager.instance.player.transform.position;
        
        if (nextSchedule==true)
        {
            if (schedule[i].leaveScene == true)
            {
                //Debug.Log("npc is out of scene");
               // DisableNPC();   
            }
            if (schedule[i].inScene == false)
            { 
                EnableNPC();
               //Debug.Log($"npc是否在场内： {schedule[i].inScene}");   
                
            }
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            if (schedule.Count == 0) { return; } 
            if (schedule[i].warp == true)
            {
               //Debug.Log("proceed to warping");
                transform.position = schedule[i].transform;

                rb2d.velocity = Vector3.zero;
                spriteRenderer.sprite = newSprite;
            }

            Vector3 moveTo = schedule[i].transform;
            if (moveTo == null) { return; }

            if (Vector3.Distance(transform.position, playerTransform) < 3f)
            {
                //靠近玩家 暂停
                animator.enabled = false;
                return;  //避免动画继续
            }

            
            animator.enabled = true;
            //玩家已经离开

            if (Vector3.Distance(transform.position, moveTo) < 1f)
            {
                //Debug.Log($"到达第{i}的时间表");
                StopMoving();
                if(NPCleaveScene == false)
                {
                   // Debug.Log("完成所有的时间表");
                    AdvanceSchedule();
                    nextSchedule = false;
                    
                    return;
                }
                else if(NPCleaveScene == true)
                {
                   
                    //将使用这个代码的对象设置为非活动状态
                    // Debug.Log($"NPC 是否离开场景： {NPCleaveScene}");
                   DisableNPC();
                    AdvanceSchedule();
                    nextSchedule = true; 
                    
                }
            }
            Vector3 direction = (moveTo - transform.position).normalized;
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);

             // Calculate the direction and normalize the result
            direction *= speed;
            rb2d.velocity = direction;
        }
        nextSchedule = false;
        
    }
    public void EnableNPC()
    {
        //Debug.Log("enabling npc");
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        animator.enabled = true;
        spriteRenderer.enabled = true;  // Show NPC
        boxCollider.enabled = true;     // Enable collisions
                   // Re-enable script
    }
    private void DisableNPC()
    {
        StopMoving();
        rb2d.bodyType = RigidbodyType2D.Static;
        //rb2d.velocity = Vector3.zero;
        animator.enabled = false;
        spriteRenderer.enabled = false;  // Hide NPC
        boxCollider.enabled = false;     // Disable collisions
                  // Disable script so NPC stops running logic
    }
    private void AdvanceSchedule()
    {
        //Debug.Log("moving on to next schedule");
        i++; // Move to the next schedule step
        if (i >= schedule.Count)
        {
            //Debug.Log("Schedule completed. NPC stops moving.");
            nextSchedule = false; // Stop further movement
        }
        else
        {
            nextSchedule = true;
        }
    }
    private void StopMoving()
    {
        //Debug.Log("stopmoving is working");
        rb2d.velocity = Vector3.zero;

        animator.enabled = false;
        spriteRenderer.sprite = newSprite;
        NPCleaveScene = schedule[i].leaveScene;
        rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    private void CheckTimeSchedule(GameTime gameTime, int i)
    {
        //Debug.Log($"current schedule for {character.Name} is {i}");
        //Debug.Log($"current position is {transform.position}");

        if (i >= schedule.Count || i < 0)
        {
           
            return; 
        }

        ScheduleTime = schedule[i].time;
        EndTime = schedule[i].endtime;
        CurrentTime = gameTime.timeText.text;
        if (CurrentTime == null)
        {
            Debug.Log("current tijme is null");
            CurrentTime = "15:00";
        }

        int currentTime = ConvertTimeToMinutes(CurrentTime);
        int scheduleTime = ConvertTimeToMinutes(ScheduleTime);
        int endTime = ConvertTimeToMinutes(EndTime);

      
        if ( currentTime >= scheduleTime && currentTime < endTime )
        {
            nextSchedule = true;
           // Debug.Log("时间表内");
            
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
    public void ResetSchedule()
    {
        i = 0; // Start from the beginning
        nextSchedule = true;
        NPCleaveScene = false;
        transform.position = initialPosition;
        rb2d.velocity = Vector2.zero;
        EnableNPC();
       // Debug.Log("Schedule has been reset.");
    }

}
