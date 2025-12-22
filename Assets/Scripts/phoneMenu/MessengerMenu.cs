using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessengerMenu :TimeAgent
{
    [SerializeField] GameObject MessengerBox;
    [SerializeField] GameObject MessengerBox2;
    [SerializeField] GameObject MessengerBox3;
    [SerializeField] GameObject messageicon;
    [SerializeField] GameObject noteicon;
    [SerializeField] GameObject phoneicon; 
    [SerializeField] GameObject NextLineButton;
    [SerializeField] GameObject ExitBtn; 
    [SerializeField] public List<GameObject> NpcChatBubble = new List<GameObject>();
    [SerializeField] public List<TextMeshProUGUI> UsernameText = new List<TextMeshProUGUI>();
    [SerializeField] public List<TextMeshProUGUI> BubbleText = new List<TextMeshProUGUI>();
    [SerializeField] public List<Image> npcProfile = new List<Image>();
    [SerializeField] public List<NPCDefintition> npcs = new List<NPCDefintition>();
    [SerializeField] GameObject NotificationButton;
    [SerializeField] AudioSource messagepop;
    [SerializeField] GameObject gcname;
    //---------------------------------------------------
    [SerializeField] GameObject PlayerMessageBox;
    [SerializeField] TextMeshProUGUI PlayerText; //the name and profile will be by default 
    public string baseText = "Typing";
    [SerializeField] Button PlayerChoice1;
    [SerializeField] Button PlayerChoice2;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] Image fader;
    public int respondIndex = -2;
    int currentMessage = 0; //which bubble
    public int contentIndex=-1;
    public bool DoneTexted = false;
    PhoneMenuController phoneMenuController;
    MessengerContainer currentContent;//which chatroom
    public List<MessengerContainer> MessagesContent = new List<MessengerContainer>();
    //gather the list of dialogue of messages that will be used in the game here 
    public void Awake()
    {
        if (MessagesContent == null)
        {
            Debug.Log("content is null");
        }
        phoneMenuController = FindObjectOfType<PhoneMenuController>();

        if (phoneMenuController == null)
        {
            Debug.LogError("PhoneMenuController not found in the scene!");
        }
      
        
    }
    public void Update()
    {
          if (player.MessengerTrigger == true)
        {
            ResetNotif();
            fader.gameObject.SetActive(false);
            
        }
    }
    public void MessengerButtonPressed()
    {

        gcname.SetActive(true);
        NextLineButton.SetActive(true);
        MessengerBox.SetActive(true);
        MessengerBox2.SetActive(true);
        MessengerBox3.SetActive(true);
        messageicon.SetActive(false);
        noteicon.SetActive(false);
        phoneicon.SetActive(false);
        NotificationButton.SetActive(false);
        Debug.Log($"current content is{ contentIndex} ");

        if(DoneTexted == false)
        {
           
            ClearMessage();
            ExitBtn.SetActive(false);
            currentContent = MessagesContent[contentIndex];
            DoneTexted = true;
            currentMessage = 0;
            respondIndex = -2;
            GetMessages(); 
            
        }
        else
        {
           
            ExitBtn.SetActive(true);
            
        }

    }

    public void GetMessages()
     {
        messagepop.Play();
        //Debug.Log($"{currentContent.line.Count}");
        if (currentContent != null && currentMessage < currentContent.line.Count)
         {
            
             if (currentMessage < 3 && respondIndex<=0)
             {
                 Debug.Log($"current messenge index :{currentContent}");
                 NpcChatBubble[currentMessage].SetActive(true);
                 npcProfile[currentMessage].gameObject.SetActive(true);
                 UsernameText[currentMessage].text = currentContent.line[currentMessage].name;
                BubbleText[currentMessage].text = currentContent.line[currentMessage].text.Replace("[name]", player.playerName);
                npcProfile[currentMessage].sprite = currentContent.line[currentMessage].profile;
                respondIndex += 1;
             }
             else 
             { //moving the previous message upward
               
                    Debug.Log("moving text upward");

                for (int i = 0; i < 2; i++)
                {
                    UsernameText[i].text = UsernameText[i + 1].text;
                    BubbleText[i].text = BubbleText[i + 1].text.Replace("[name]", player.playerName); 
                    npcProfile[i].sprite = npcProfile[i + 1].sprite;

                }

                  UsernameText[2].text = currentContent.line[currentMessage].name;
                  BubbleText[2].text = currentContent.line[currentMessage].text.Replace("[name]", player.playerName); 
                  npcProfile[2].sprite = currentContent.line[currentMessage].profile;

                  //after displaying the current message and found the playernext boolean 
                  if (currentContent.line[currentMessage].playerNext == true)
                  {
                      for (int i = 0; i < 2; i++)
                      {
                          UsernameText[i].text = UsernameText[i + 1].text;
                          BubbleText[i].text = BubbleText[i + 1].text.Replace("[name]", player.playerName);
                        npcProfile[i].sprite = npcProfile[i + 1].sprite;
                      }
                    
                      NpcChatBubble[2].SetActive(false);
                      npcProfile[2].gameObject.SetActive(false);
                      PlayerMessageBox.SetActive(true);

                      StartCoroutine(AnimateTyping());
                      PlayerTurn();

                  }
               

             }

         }
         else if(currentMessage == currentContent.line.Count)
         {
             player.TodayTexted = true;
             ExitBtn.SetActive(true);   
         }


     }
    public void NextLineMessage()
    {
        currentMessage += 1;
        GetMessages();
        
    }
 
    private IEnumerator AnimateTyping()
    {
        while (true)
        {
            PlayerText.text = "";
            PlayerText.text = baseText + ".";
            yield return new WaitForSeconds(0.5f);

            PlayerText.text = baseText + "..";
            yield return new WaitForSeconds(0.5f);

            PlayerText.text = baseText + "...";
            yield return new WaitForSeconds(0.5f);

            PlayerText.text = baseText; // Reset
            yield return new WaitForSeconds(0.5f);
        }

    }

    public void PlayerTurn()
    {
        PlayerChoice1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentContent.choices[0].ChoiceText;
        PlayerChoice2.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentContent.choices[1].ChoiceText;
        PlayerChoice1.gameObject.SetActive(true);
        PlayerChoice2.gameObject.SetActive(true);
        //prevent system confusion
        PlayerChoice1.onClick.RemoveAllListeners();
        PlayerChoice1.onClick.AddListener(() => ChooseDialogue(0));
        PlayerChoice2.onClick.RemoveAllListeners();
        PlayerChoice2.onClick.AddListener(() => ChooseDialogue(1));
    }

    private void ChooseDialogue(int choiceIndex)
    {

        MessengerContainer nextDialogue = currentContent.choices[choiceIndex].messengerContainer;
        float scores = currentContent.choices[choiceIndex].score;
        float currentScores = currentContent.choices[choiceIndex].npc.DailyData.level;
        currentScores = currentScores + scores;
        Debug.Log($"{currentScores}");
           

   
        // Hide the choice buttons
        PlayerChoice1.gameObject.SetActive(false);
        PlayerChoice2.gameObject.SetActive(false);
       
        // Start the selected dialogue
        Initialize(nextDialogue);
    }

    public void Initialize(MessengerContainer newMessengerContent)
    { 
        
       // Debug.Log("initialize class is working");
        currentContent = newMessengerContent;
        currentMessage = 0;
        respondIndex = 1;
        PlayerMessageBox.SetActive(false);
        NpcChatBubble[2].SetActive(true);
        npcProfile[2].gameObject.SetActive(true);
       

        GetMessages();
       
    }
    public void ClearMessage()
    {
        for (int i = 0; i < 3; i++)
        {
            UsernameText[i].text = "";
            BubbleText[i].text = "";
            NpcChatBubble[i].SetActive(false);
            npcProfile[i].gameObject.SetActive(false);
            
        }
    }
    public void ExitButton()
    {
        
        phoneMenuController.ShowMenu();
    }
   void ResetNotif()
    {
        contentIndex += 1;
        player.MessengerTrigger = false;
        
        Debug.Log($"finished school , loading messanges , content{contentIndex}");
        
        NotificationButton.SetActive(true);
        DoneTexted = false;
    }

}
