using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;
    [SerializeField] Text nameText;
    [SerializeField] Image portrait;
    [SerializeField] Button Option1Text;
    [SerializeField] Button Option2Text;
    [SerializeField] DialogueContainer currentDialogue;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] AudioSource TypingSound;
    NPCCharacter npcCharacter;
    InventoryButton inventoryButton;
    int currentTextLine;
    public float scores; 
    //showing each letter one by one instead of showing whole dialog
    [Range(0f,1f)]
    [SerializeField] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    string lineToshow;
   
    private void Start()
    {
        npcCharacter = FindObjectOfType<NPCCharacter>();

    }

    private void Update()
    {
        
       if(Input.GetKeyDown(KeyCode.Z))
        {
            PushText();
        }
        TypeOutText();

        
    }
   
    private void TypeOutText()
    {
        if(string.IsNullOrEmpty(lineToshow) || visibleTextPercent >= 1f) { return; }
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0, 1f);
        UpdateText();
        if (visibleTextPercent >= 1f && TypingSound.isPlaying)
        {
            TypingSound.Stop();
        }
    }
    void UpdateText()
    {
        if (string.IsNullOrEmpty(lineToshow)) return;
        int letterCount = (int)(lineToshow.Length * visibleTextPercent);
        targetText.text = lineToshow.Substring(0, letterCount);
    }
    private void PushText() //determine the end of the dialog 
    {
        
        if(visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            if (TypingSound.isPlaying)
                TypingSound.Stop();

           
            return;
        }

        if (currentTextLine == currentDialogue.line.Count)
        {
           // Debug.Log("no more line to display");
            Conclude();
        }
        else
        {   currentTextLine+=1;
          //  Debug.Log($"current line is{currentTextLine}, dialogue line {currentDialogue.line.Count}");
            CycleLine();
        }
    }
    //proceed to the next line
    void CycleLine()
    {
        if (currentDialogue.dialogueType == DialogueContainer.DialogueType.normal)
        {
           // Debug.Log($"normal dialogue, current line {currentTextLine}");
            lineToshow = currentDialogue.line[currentTextLine].text;
            totalTimeToType = lineToshow.Length * timePerLetter;
            currentTime = 0f;
            visibleTextPercent = 0f;
            targetText.text = " ";
            UpdatePortrait();
            //currentTextLine += 1;
            TypingSound.volume = 0.4f;
            TypingSound.loop = true;     // keeps it playing
            TypingSound.Play();
            Option1Text.gameObject.SetActive(false);
            Option2Text.gameObject.SetActive(false);

            
            

        }
        else if (currentDialogue.dialogueType == DialogueContainer.DialogueType.quest)
        {
            //Debug.Log($"this is quest dialogues, line number{currentTextLine}");
            lineToshow = currentDialogue.line[currentTextLine].text;
            totalTimeToType = lineToshow.Length * timePerLetter;
            currentTime = 0f;
            visibleTextPercent = 0f;
            targetText.text = " ";
            UpdatePortrait();
            //setting up the answer with the button
            Option1Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentDialogue.choices[0].ChoiceText;
            Option2Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentDialogue.choices[1].ChoiceText;
            Option1Text.gameObject.SetActive(true);
            Option2Text.gameObject.SetActive(true);
            Option1Text.onClick.RemoveAllListeners();
            Option1Text.onClick.AddListener(() => ChooseDialogue(0));

            Option2Text.onClick.RemoveAllListeners();
            Option2Text.onClick.AddListener(() => ChooseDialogue(1));
        }

    }
    private void ChooseDialogue(int choiceIndex)
    {
        
        DialogueContainer nextDialogue = currentDialogue.choices[choiceIndex].dialogueContainer;
        scores = currentDialogue.choices[choiceIndex].score;
        //Debug.Log($"current score is{scores}");
        npcCharacter.IncreaseRelationship(scores);
        // Hide the choice buttons
        Option1Text.gameObject.SetActive(false);
        Option2Text.gameObject.SetActive(false);
       
        // Start the selected dialogue
        Initialize(nextDialogue);
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {

        GameManager.instance.gameTime.PauseTime(); 
        Show(true);
        currentDialogue = dialogueContainer;
      
        currentTextLine = 0;

        // show only after CycleLine sets things up
        CycleLine();
        //visibleTextPercent = 1f;  
        //UpdateText();

    }
    private void UpdatePortrait()
    {
        //Debug.Log("inside update portrait");
        if (currentDialogue.line[currentTextLine].expression != null)
        {//since expression is originally 2dtexture , need to change it to portrait first for unity ui
            portrait.sprite = Sprite.Create(
                currentDialogue.line[currentTextLine].expression,
                new Rect(0, 0, currentDialogue.line[currentTextLine].expression.width, currentDialogue.line[currentTextLine].expression.height),
                Vector2.one * 0.5f
            );
        }
        nameText.text = currentDialogue.actor.Name;
    }
    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }
    public void Conclude()
    {
        
       // Debug.Log("dialogue is over");
        Show(false);
        if (currentDialogue.StoryItemTrigger == null || currentDialogue.StoryItemTrigger == "")
        {
           // Debug.Log("storytrigger in this dialogue is null");

        }
        else
        {
            //Debug.Log($"trigger is :{currentDialogue.StoryItemTrigger}");
            player.currentTrigger = currentDialogue.StoryItemTrigger;
            return;
        }
        GameManager.instance.gameTime.ResumeTime();

    }
}
