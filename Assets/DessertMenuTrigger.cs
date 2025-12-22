using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DessertMenuTrigger : MonoBehaviour
{
    [SerializeField] GameObject DessertMenu;
    [SerializeField] GameObject MenuButtons;
    [SerializeField] GameObject OrderButtons;
    [SerializeField] PlayerScoreRecord player;
    //[SerializeField] CustomerManager customer;
    [SerializeField] CustomerNPC availableMenu;
    [SerializeField] List<Button> button;
    [SerializeField] List<Button> orderbtn;
    [SerializeField] List<DialogueContainer> dialogue;
    MinigameController minigameController;
    bool MenuShowed = false;
   // MinigameController minigameController;
    private Character character;
    public bool GetDessert = false;
    private ServingDessert serving; 
    private void Awake()
    {
        DessertMenu.SetActive(false);
        serving = FindObjectOfType<ServingDessert>();
        if (serving == null)
        {
            Debug.Log("cant find taking serving script");
        }
        //OrderMenu.SetActive(false);
    }

    private void Start()
    {
        character = FindObjectOfType<Character>();

    }

    public void DisplayMenu()
    {
        DessertMenu.SetActive(true);
        OrderButtons.SetActive(false);
        MenuButtons.SetActive(true);
        for (int i = 0; i < availableMenu.OrderDialogue.Count; i++)
        {
            Sprite dessertSprite = availableMenu.OrderDialogue[i].dessert;
            string name = availableMenu.OrderDialogue[i].DessertName;
            button[i].GetComponent<Image>().sprite = dessertSprite;

            button[i].GetComponentInChildren<TextMeshProUGUI>().text = name;

        }
        MenuShowed = true;
        player.PartTimeOnDay += 1;
        player.DidParttimeToday = true;
    }

    public void DisplayOrder()
    {
        MenuButtons.SetActive(false);
        DessertMenu.SetActive(true);
        OrderButtons.SetActive(true);
        for (int i = 0; i < availableMenu.OrderDialogue.Count; i++)
        {
         
            string name = availableMenu.OrderDialogue[i].DessertName;
            //orderbtn[i].GetComponent<Image>().sprite = null;
            orderbtn[i].GetComponentInChildren<TextMeshProUGUI>().text = name;
            orderbtn[i].onClick.RemoveAllListeners();

            // Add a new listener
            orderbtn[i].onClick.AddListener(() => CheckDessertChoice(name));
        }
    }
    private void CheckDessertChoice(string selectedDessert)
    {
        DessertMenu.SetActive(false);
    
        if (selectedDessert == player.currentDessert)
        {
            GameManager.instance.dialogueSystem.Initialize(dialogue[0]);
            player.correct = true;
    
        }
        else
        {
            GameManager.instance.dialogueSystem.Initialize(dialogue[1]);
            
            player.correct = false;

        }
        player.currentCooking = player.currentDessert;
        player.ordered = false;
        player.FirstOrder = false;
        serving.PreparingDessert();
    }
    public void CloseTab()
    {
        
        DessertMenu.SetActive(false);
        if (MenuShowed == true)
        {
            MenuButtons.SetActive(false);
            MenuShowed = false;
        }
        
    }
}
