using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.RestService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject fadeImage;
    
    [SerializeField] GameObject toolbar;
    [SerializeField] GameObject HpBar;
    [SerializeField] AudioSource clickSound;
    [SerializeField] ChloeLetterSecondAttempt letter;
    public static GameManager instance;
    [SerializeField] PlayerScoreRecord data;
    // public GameObject canvas;
    [SerializeField] AudioSource soundtrack;
    BoxCollider2D box;
    private void Awake()
    {
        instance = this;
        soundtrack.loop=true;
        soundtrack.Play();
        /*if (data.InMiniGameScene != true)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
      */sleep = player.GetComponent<Sleep>();
        box=player.GetComponent<BoxCollider2D>();
        
    }
    public Sleep sleep;
    public GameObject player;
    public ItemContainer inventoryContainer; 
    public ItemDragAndDropManager dragAndDropManager;
    public DialogueSystem dialogueSystem;
    public ScreenFader screenFader;
    public GameTime gameTime;
    public CutSceneManager cutSceneManager;
    public DisableControls disableControls;
    public QuestDisplay quest;
    public void DisableFade()
    {
        fadeImage.SetActive(false);
    }
    public void DisablePlayerBox()
    {
        
        if (box != null)
        {
           
            box.enabled = false;
        }
        else
        {
            Debug.Log("box is null");
        }
    }
    public void EnablePlayerBox()
    {
        if (box != null)
        {
            box.enabled = true;
        }
    }
    public void DisableSoundtrack()
    {
        soundtrack.Stop();
    }
    public void EnableSoundtrack()
    {
        soundtrack.loop = true;
     
        soundtrack.Play();
    }
    public void ClickButtonSound()
    {
        clickSound.Play();
    }
    public void minigameDisable()
    {
        //Debug.Log("disabling the panel in essential");
        toolbar.SetActive(false);
        HpBar.SetActive(false);
    }
    public void minigameEnable()
    {
        toolbar.SetActive(true);
        HpBar.SetActive(true);
    }
    public void ChloeLetterCheck()
    {
        data.chloeletterInHand = true;
        letter.RecheckChloeLetter();
    }
    public static void DestroyInstance()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }
}

