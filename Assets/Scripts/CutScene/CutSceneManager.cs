using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CutSceneManager : MonoBehaviour
{
    public Image cutSceneImage;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] GameObject messageBoxUI;
    [SerializeField] private TMPro.TextMeshProUGUI messageText;
    [SerializeField] List<CutSceneDialogue> cutscene;
    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;
    [SerializeField] float timePerLetter = 0.05f;
    [SerializeField] Button pushbutton;
    [SerializeField] Button Option1Text;
    [SerializeField] Button Option2Text;
    [SerializeField] GameObject ChloeLetter;
    [SerializeField] Image portrait;
    [SerializeField] GameObject EndingMenu;
    [SerializeField] TMPro.TextMeshProUGUI EndingText;
    [SerializeField] AudioSource TypingSound;
    [SerializeField] GameObject CutScenePanel;
    [SerializeField] string relocationScene = "mc_house";
    [SerializeField] Vector3 target; 
    string MenuName = "MainMenu"; 
    float totalTimeToType, currentTime;
    CutSceneDialogue currentCutScene;
    int getCutsceneno;
    int currentLine = 0;
    string lineToshow;
    bool sameImage;
    bool pushPressed = false;
    bool nextTrigChloeLetter=false;
    GameTime gameTime;
    NPCCharacter npcCharacter;
    public bool concludedialog;
    AsyncOperation operation;
    public void Awake()
    {
        //messageBoxUI.SetActive(false);
        gameTime = GameManager.instance.gameTime;
        Option1Text.gameObject.SetActive(false);
        Option2Text.gameObject.SetActive(false);
        ChloeLetter.SetActive(false); //most of the game state
        if(player.currentTrigger == "ChloeLetterSecond") //if awake during the time player has chloe trigger.
        {
            ChloeLetter.SetActive(true);
        }
        npcCharacter = FindObjectOfType<NPCCharacter>();
        if (npcCharacter == null)
        {
            Debug.Log("npc is null in cutscene manager");
        }
    }
    private void Update()
    {
        if (pushPressed)
        {
            PushText();
            pushPressed = false; // Reset the flag
        }

        TypeOutText();

    }
    public void PushBtn()
    {
        GameManager.instance.ClickButtonSound();
        pushPressed = true;
     
    }
    public void GetCutsceneIndex(int i)
    {
        currentCutScene = cutscene[i];
        Initialize(currentCutScene);
    }
    private void TypeOutText()
    {
        if (visibleTextPercent >= 1f) { return; }
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
        if (string.IsNullOrEmpty(lineToshow) || messageText == null) return;

        int letterCount = (int)(lineToshow.Length * visibleTextPercent);
        messageText.text = lineToshow.Substring(0, letterCount);
    }
    private void PushText() //determine the end of the dialog 
    {

        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            if (TypingSound.isPlaying)
                TypingSound.Stop();
            return;
        }
        if (currentLine >= currentCutScene.dialogue.Count)
        {
            //Debug.Log($"is this cutscene selectable: {currentCutScene.Selectable}");
            if (currentCutScene.Selectable == true)
            {
                EnableChoiceButton();
            }
            else
            {
                //Debug.Log("no more line to display");
                Conclude();
                cutSceneImage.gameObject.SetActive(false);
            }
            
        }
        else
        {
            CutSceneProcess();
        }
    }
    public void CutSceneProcess()
    {
       
        lineToshow = currentCutScene.dialogue[currentLine].line;
        totalTimeToType = lineToshow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;
       
        if(currentCutScene.dialogue[currentLine].ChangeImage == true)
        {
            pushbutton.gameObject.SetActive(false);
            StartCoroutine(ChangeImage());
        }
        
        messageText.text = " ";
        UpdatePortrait();
        TypingSound.volume = 0.2f;
        TypingSound.loop = true;     // keeps it playing
        TypingSound.Play();
        currentLine += 1;

    }
    private void UpdatePortrait()
    {
        if (currentCutScene.dialogue[currentLine].npc != null)
        {
            portrait.sprite = Sprite.Create(
               currentCutScene.dialogue[currentLine].npc,
               new Rect(0, 0, currentCutScene.dialogue[currentLine].npc.width, currentCutScene.dialogue[currentLine].npc.height),
               Vector2.one * 0.5f
           );
        }
    }
    private void EnableChoiceButton()
    {
        portrait.gameObject.SetActive(false);
        // Debug.Log("Enabling choice buttons");
        messageText.gameObject.SetActive(false);
        Option1Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentCutScene.options[0].Option;
        Option2Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentCutScene.options[1].Option;
        pushbutton.gameObject.SetActive(false);

        Option1Text.gameObject.SetActive(true);
        Option2Text.gameObject.SetActive(true);
        Option1Text.onClick.RemoveAllListeners();
        Option1Text.onClick.AddListener(() => ChooseOption(0));

        Option2Text.onClick.RemoveAllListeners();
        Option2Text.onClick.AddListener(() => ChooseOption(1));
    }
    private void ChooseOption(int choiceIndex)
    {
        GameManager.instance.ClickButtonSound();
        portrait.gameObject.SetActive(true);
        pushbutton.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
        CutSceneDialogue nextDialogue = currentCutScene.options[choiceIndex].OptionDialogue;
       // Debug.Log("calculating npc score happening here");
        npcCharacter.IncreaseRelationship(currentCutScene.options[choiceIndex].Score);
        // Hide the choice buttons
        
        Option1Text.gameObject.SetActive(false);
        Option2Text.gameObject.SetActive(false);
        
        // Start the selected dialogue
        Initialize(nextDialogue);
    }
    public void UpdateBackground()
    {
        if (currentCutScene.dialogue[currentLine].noBackground == false)
        {
            if (currentCutScene.dialogue[currentLine].background != null)
            {
                cutSceneImage.sprite = currentCutScene.dialogue[currentLine].background;
               // Debug.Log($"Background set to: {cutSceneImage.sprite.name}");
                pushbutton.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Background is null for this dialogue line.");
            }

        }
        
    /*
    if (currentLine < currentCutScene.dialogue.Count)
    {
        if (currentCutScene.dialogue[currentLine].background != null)
        {
            cutSceneImage.sprite = currentCutScene.dialogue[currentLine].background;
            Debug.Log($"Background set to: {cutSceneImage.sprite.name}");
            pushbutton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Background is null for this dialogue line.");
        }
    }
    else
    {
        Debug.LogWarning("currentLine index is out of range.");
    }*/
}
    public void BackToMenuButton()
    {
        GameManager.instance.ClickButtonSound();
        SceneManager.LoadScene(MenuName, LoadSceneMode.Single);
        
    }
    public void Initialize(CutSceneDialogue cutsceneno)
    {
        gameTime.PauseTime();
        //Debug.Log($"current dialog is{cutsceneno.cutscenename}");
        Show(true);
        CutScenePanel.SetActive(true);
        currentLine = 0;
        currentCutScene = cutsceneno;
        concludedialog = false; //using this to trigger second cutscene in heeseungconfront
        CutSceneProcess();

    }
    private void Show(bool v)
    {
        messageBoxUI.SetActive(v);
    }

    public void Conclude()
    {
     
        if (currentCutScene.EndingCutscene == false)
        {
            Show(false);
            CutScenePanel.SetActive(false);
            gameTime.ResumeTime();
            cutSceneImage.gameObject.SetActive(false);
            if (currentCutScene.nextTrigger == true)
            {
                player.currentTrigger = currentCutScene.NextTriggerName;
                if (player.currentTrigger == "ChloeLetterSecond")
                {
                    nextTrigChloeLetter = true;
                }
            }
            else
            {
                player.currentTrigger = null;
                StartCoroutine(ChangingDay());
                
            }
            if (nextTrigChloeLetter == true)
            {
                //SystemMessengerBox.Instance.ShowMessage("Chloe's Letter shortcut has been added to the screen");
                ChloeLetter.SetActive(true);

            }
            concludedialog = true; 
           
        }
        if (currentCutScene.EndingCutscene == true)
        {
            Show(false);
            EndingMenu.gameObject.SetActive(true);
            EndingText.text = currentCutScene.NextTriggerName;

        }
       
    
    }
    IEnumerator ChangingDay()
    {
        GameManager.instance.disableControls.DisableControl();
        GameManager.instance.screenFader.Tint();
        SceneTransitionManager.Instance.InitSwitchScene(relocationScene, target);
        yield return new WaitForSeconds(6f);
        
        
        //GameManager.instance.screenFader.UnTint();
        GameManager.instance.gameTime.SkipToMorning();
        //yield return new WaitForSeconds(2f);
        GameManager.instance.disableControls.EnableControl(); 
        yield return null;
    }
    IEnumerator ChangeImage()
    {
        //Debug.Log("before tinting");
        cutSceneImage.gameObject.SetActive(true);
        ScreenFader screenFader = GameManager.instance.screenFader;
        screenFader.Tint();
        yield return new WaitUntil(() => screenFader.image.color == screenFader.tintedColor);
       // Debug.Log("after tinting,before checking no background state ");
        if (currentCutScene.dialogue[currentLine].noBackground == true)
        {
            //Debug.Log("inside checking");
            CutScenePanel.gameObject.SetActive(false);
            pushbutton.gameObject.SetActive(true);
        }
        else if(currentCutScene.dialogue[currentLine].noBackground == false)
        {
            //Debug.Log("updating the background image");
            UpdateBackground();
        }
        //Debug.Log("after checking");
        yield return new WaitForSeconds(0.1f);

        // Start fading in and wait until it's done
        screenFader.UnTint();

        yield return null;
    }
}
