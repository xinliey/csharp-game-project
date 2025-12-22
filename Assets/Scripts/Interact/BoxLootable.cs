using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BoxLootable : Interactable
{ //this script will be used with the iteractable object for looting or clues for quests
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;
    [SerializeField] string displayText;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] GameObject sparkle;
    private SpriteRenderer spriteRenderer;
    public bool hideitem;
    public bool SystemMessage;
    public bool cutscene;
    public string cutsceneName;
    public int index;
    BoxCollider2D box2d;
   
    public void Awake()
    {
        sparkle.SetActive(false);
        box2d = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (cutscene == true)
        {
            if (player.currentTrigger != cutsceneName )
            {
                box2d.enabled = false;
                if (hideitem == true)
                {
                    spriteRenderer.enabled = false;
                }
            }
            else
            {
                if (player.quest[index].isDone == true)
                {
                    box2d.enabled = false;
                }
                else
                {
                    box2d.enabled = true;
                    sparkle.SetActive(true);
                }
            }
            
        }
    }
    public override void Interact(Character character)
    {
        Debug.Log("Interact inside box");
        if (SystemMessage == false)
        {

            if (cutscene != true)
            {
                while (dropCount > 0)
                {
                    dropCount -= 1;
                    Vector3 position = transform.position;
                    position.x += spread * UnityEngine.Random.value - spread / 2;
                    position.y -= 2.0f;
                    //spread * UnityEngine.Random.value - spread / 2;
                    //Debug.Log($"Drop position: {position}");

                    GameObject go = Instantiate(pickUpDrop);
                    go.transform.position = position;



                }
                Destroy(gameObject);
            }
            else
            {
               
                if (player.quest[index].isDone == false)
                {
                    CheckCutSceneState();
                    while (dropCount > 0)
                    {
                        dropCount -= 1;
                        Vector3 position = transform.position;
                        position.x += spread * UnityEngine.Random.value - spread / 2;
                        position.y -= 2.0f;
                        //spread * UnityEngine.Random.value - spread / 2;
                        //Debug.Log($"Drop position: {position}");

                        GameObject go = Instantiate(pickUpDrop);
                        go.transform.position = position;



                    }
                    player.quest[index].isDone = true;
                    sparkle.SetActive(false);
                    Destroy(gameObject);
                }
                else
                {
                    return;
                }
            }
            

        }
        else
        {
            if (cutscene == true)
            {
                CheckCutSceneState();
            }
            else
            {
                SystemMessengerBox.Instance.ShowMessage(displayText);
            }
           


        }
    }
   private void CheckCutSceneState()
    {
        Debug.Log("is a quest item");
        if (player.currentTrigger == cutsceneName)
        {
            Debug.Log("currently triggering");
                GameManager.instance.cutSceneManager.Initialize(player.quest[index].cutscene);
                player.quest[index].isDone = true;
            sparkle.SetActive(false);
        }
        else
        {
            if (displayText != null)
            {
                SystemMessengerBox.Instance.ShowMessage(displayText);
            }
            else
            {
                Debug.Log("display text is null");
            }
        }
       
    }
}
