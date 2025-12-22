using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject fadeImage;
    [SerializeField] GameObject toolbar;
    [SerializeField] GameObject HpBar;
    [SerializeField] AudioSource clickSound;
    public static GameManager instance;
    [SerializeField] PlayerScoreRecord data;
   // public GameObject canvas;
   
    private void Awake()
    {
        instance = this;
        /*if (data.InMiniGameScene != true)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
      */
        
    }
    public GameObject player;
    public ItemContainer inventoryContainer; 
    public ItemDragAndDropManager dragAndDropManager;
    public DialogueSystem dialogueSystem;
    public ScreenFader screenFader;
    public GameTime gameTime;
    public CutSceneManager cutSceneManager;
    public DisableControls disableControls;
    public void DisableFade()
    {
        fadeImage.SetActive(false);
    }
    public void ClickButtonSound()
    {
        clickSound.Play();
    }
    public void minigameDisable()
    {
        Debug.Log("disabling the panel in essential");
        toolbar.SetActive(false);
        HpBar.SetActive(false);
    }
    public void minigameEnable()
    {
        toolbar.SetActive(true);
        HpBar.SetActive(true);
    }
}

