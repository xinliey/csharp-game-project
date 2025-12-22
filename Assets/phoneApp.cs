using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phoneApp : MonoBehaviour
{

    [SerializeField] List<Button> contacts = new List<Button>();
    [SerializeField] List<NPCDefintition> npcs = new List<NPCDefintition>();
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] GameObject panel;
    [SerializeField] PhoneMenuController phone;
 
    public AudioSource hintSound;
    public AudioSource pickupPhone;
    public AudioSource busyline;
   
    private void Awake()
    {
        for(int i = 0; i < contacts.Count; i++)
        {
            int index = i;
            contacts[i].onClick.RemoveAllListeners();
            contacts[i].onClick.AddListener(() => ContactBtn(index));
        }
    }
    public void phoneclicked()
    {
        panel.SetActive(true);   
    }
    public void ContactBtn(int i)
    {
        //Debug.Log($"button{i} is being triggered");
        //Debug.Log($"the current dialogue belongs to {npcs[i].Name}");

        if (player.currentTrigger == "ChloeLetterSecond")
        {
            if (i == 0)
            {//if player call heeseung
                player.currentTrigger = "HeeseungHouse";
            }
            else
            {
               // player.currentTrigger = "NextVictim";
                player.closestnpc = npcs[i];
                Debug.Log($"next victim is {npcs[i].Name}");
            }
            if (npcs[i].LoreDialogues[4] != null)
            {
                StartCoroutine(PlaySoundsAndStartDialogue(npcs[i].LoreDialogues[4]));
            }
            else
            {
                Debug.Log("dialogue is null");
            }
            
        }
        else
        {
            StartCoroutine(BusyLine());
        }
        
    }
    private IEnumerator PlaySoundsAndStartDialogue(DialogueContainer dialog)
    {


        
        hintSound.Play(); // Play first sound
        yield return new WaitWhile(() => hintSound.isPlaying); // Wait until first sound finishes
        pickupPhone.Play(); // Play second sound
        yield return new WaitWhile(() => pickupPhone.isPlaying);
        phone.CloseMenu();
        GameManager.instance.dialogueSystem.Initialize(dialog);
        
    }
    private IEnumerator BusyLine()
    {
        
        hintSound.Play(); // Play first sound
        yield return new WaitWhile(() => hintSound.isPlaying); // Wait until first sound finishes
        busyline.Play();
        yield return new WaitWhile(() => busyline.isPlaying); 
        
        SystemMessengerBox.Instance.ShowMessage("He is not picking up");
       phone.CloseMenu();
    }
}
