using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableReward : Interactable
{
    SchoolTable table;
    [SerializeField] GameObject MinigamePanel;
    [SerializeField] GameObject gamebtn;
    [SerializeField] GameObject scoretext;
    [SerializeField] GameObject backbtn;
    [SerializeField] TMPro.TextMeshProUGUI score;
    [SerializeField] PlayerScoreRecord data;
    [SerializeField] List<Item> rewardItems;
    [SerializeField] List<Image> ItemDisplay;
    [SerializeField] GameObject itemPanel; 
    [SerializeField] ItemPanel inventoryItemPanel;
    [SerializeField] ItemContainer playerInventory;
    [SerializeField] Vector3 minigameTransform;
   
    private bool canGetReward = false;
    private bool isOpened = false;
    private bool rewardReceived = false;
    
    string newScene = "SchoolMinigame";
    private void Awake()
    {
        table = GetComponent<SchoolTable>();
        if (table == null)
        {
            Debug.Log("table script is not fount");
        }
       
        
        MinigamePanel.SetActive(false);
        scoretext.SetActive(false);
        itemPanel.SetActive(false);
        score.gameObject.SetActive(false);
    }
    public override void Interact(Character character)
    {
      // Debug.Log("interact with table");
        if (isOpened == false)
        {
            MinigamePanel.SetActive(true);
            backbtn.SetActive(false);
            GameObject player = GameObject.FindGameObjectWithTag("GameController");
            if (player != null)
            {
                //Debug.Log("getting inventory panel from essential");
                inventoryItemPanel = player.GetComponent<ItemToolBarPanel>();
                //Debug.Log("successfully insert player item panel into table reward script");
            }
            else
            {
                Debug.Log("player is null");
            }
            isOpened = true;
        }
        else
        {
            MinigamePanel.SetActive(false);
            isOpened = false;
        }
    }

    public void MinigameStart()
    {
        if (data.finishedSchool == false)
        {
            data.InMiniGameScene = true;
            SceneTransitionManager.Instance.InitSwitchScene(newScene, minigameTransform);
        }
        else
        {
            SystemMessengerBox.Instance.ShowMessage("it's good to see you're working hard, but you gotta rest too");
        }
        
    }

    public void BackBtnPressed()
    {
        gamebtn.SetActive(true);
        scoretext.SetActive(false);
        backbtn.SetActive(false);
        itemPanel.SetActive(false);
        score.gameObject.SetActive(false);
    }
    public void CheckScore()
    {
        gamebtn.SetActive(false);
        score.text = data.wordguessScore.ToString();
        scoretext.SetActive(true);
        score.gameObject.SetActive(true);
        backbtn.SetActive(true);
        int index = data.wordguessScore / 5;
        if (index >= data.wordguessReward) //make sure word guess reward start from 1not 0
        {//making sure score more than 5 get to claim reward of 5 
           // Debug.Log($"index is {index}");
            canGetReward = true;

        }
        else
        {
            canGetReward = false;
           // Debug.Log("resetting receive reward statement");
        }
        DisplayReward();

    }
    public void CheckScoreForNext()
    {
        int index = data.wordguessScore / 5;
       // Debug.Log($"current reward is{data.wordguessReward}");
        if (index >= data.wordguessReward+1) //make sure word guess reward start from 1not 0
        {//making sure score more than 5 get to claim reward of 5 
            Debug.Log($"index is {index}");
            canGetReward = true;
            ReceiveReward();
        }
        else
        {
            canGetReward = false;
           // Debug.Log("resetting receive reward statement");
        }
        DisplayReward();
    }
    public void DisplayReward()
    {
        itemPanel.SetActive(true); //display available reward 
        for(int i = 0; i <ItemDisplay.Count; i++)
        {
            
            ItemDisplay[i].sprite = rewardItems[i].icon;
            if(i< data.wordguessReward)
            {
                Color faded = ItemDisplay[i].color;

                faded.a = 0.3f;
                ItemDisplay[i].color = faded;
            }

        }

    }
    

    public void ReceiveReward()
    {
        
        if (canGetReward == false)
        {
            SystemMessengerBox.Instance.ShowMessage("your score isn't enough to claim reward");
            return;
        }
        else
        {
            if (rewardReceived == false)
            {
                Item itemToGet = rewardItems[data.wordguessReward  ];
                playerInventory.Add(itemToGet);
                inventoryItemPanel.Show(); 
                Color faded = ItemDisplay[data.wordguessReward].color;

                faded.a = 0.3f;
                ItemDisplay[data.wordguessReward].color = faded;

                data.wordguessReward += 1;
              //  rewardReceived = true;

            }
            else
            {
                Debug.Log("玩家已取走奖品");
            }
        }   
    }
}
