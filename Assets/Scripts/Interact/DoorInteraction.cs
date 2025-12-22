using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interactable
{
    [SerializeField] AudioSource DoorSound;
    //public string targetSceneName;      // Name of the target scene
    //public Vector3 targetSpawnPosition; // Position where the player will spawn in the new scene
    Transition transition;

    public override void Interact(Character character)
    {
        DoorSound.Play();
        transition = GetComponent<Transition>();
        if(transition != null)
        {
            Debug.Log("Sending signal to transition script");
            transition.InitiateTransition(character.transform);

        }
        
    }

}
