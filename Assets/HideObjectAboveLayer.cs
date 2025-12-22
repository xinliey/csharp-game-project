using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideObjectAboveLayer : Interactable
{

    NPCCharacter npc;
    [SerializeField] GameObject ownerRoom;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] TilemapRenderer HidingObject;
    Transition transition;
    private void Awake()
    {
        npc = ownerRoom.GetComponent<NPCCharacter>();
    }
    public override void Interact(Character character)
    {
        transition = GetComponent<Transition>();
        transition.DoorTransition(character.transform);
            HidingObject.gameObject.SetActive(false);
        if (npc.CurrentLevel >= 3)
        {
            
        }
        else
        {
            SystemMessengerBox.Instance.ShowMessage("your level is too low to be entering this room");

        }

    }
}
