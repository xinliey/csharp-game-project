using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SchoolTable : Interactable
{ 
    public bool cafe;
    Transition transition;
    public string minigame;

    public override void Interact(Character character)
    {
         Debug.Log("interacting with table");
        SystemMessengerBox.Instance.ShowMessage($"{minigame}");
        transition = GetComponent<Transition>();
        if (transition != null)
        {
            Debug.Log("Sending signal to transition script");
            transition.InitiateTransition(character.transform);

        }
    }
 

   
}
