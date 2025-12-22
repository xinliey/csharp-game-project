using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCRoom : Interactable
{
    NPCCharacter npc;
    [SerializeField] GameObject ownerRoom;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] GameObject sparkle;
    
    public int questIndex;//for cutscene
    public string enterTrigger;
    public string PlayerCurrentItem;
    Transition transition;
    public bool questRoom;
    public bool cutscenewheneneter;
    public bool HideLayer; //some room need to hide object above layer, use with 
                           //objectabovelayer script. 
    [SerializeField] TilemapRenderer layerToHide;
   // public bool normalEnter; 
    private void Awake()
    {
        sparkle.SetActive(false);
        PlayerCurrentItem = player.currentTrigger;
        npc = ownerRoom.GetComponent<NPCCharacter>();
        if (cutscenewheneneter == true)
        {
            if (npc.character.Questcutscene[questIndex].isDone == true)
            {
                questRoom = false;
            }

        }
        if (questRoom == true && enterTrigger == PlayerCurrentItem)
        {
            sparkle.SetActive(true);
        }
        if (HideLayer == false)
        {
            layerToHide = null;
            return; 
        }
        
    }
    public override void Interact(Character character)
    {
        transition = GetComponent<Transition>();

        if (questRoom == false)
        {

            if (npc.CurrentLevel >= 2)
            {
                transition.DoorTransition(character.transform);
                if (HideLayer == true)
                {
                    layerToHide.gameObject.SetActive(false);
                }
            }
            else
            {
                SystemMessengerBox.Instance.ShowMessage("your level is too low to be entering this room");

            }

        }
        else 
        {
            if (enterTrigger == PlayerCurrentItem) 
            {
                if (cutscenewheneneter == true)
                {
                    GameManager.instance.cutSceneManager.Initialize(npc.character.Questcutscene[questIndex].cutscene);
                    npc.character.Questcutscene[questIndex].isDone = true;
                    sparkle.SetActive(false);
                }
                transition.DoorTransition(character.transform);
            }
           
            
            
        }
       


    }
}
