using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableControls : MonoBehaviour
{
    PlayerController characterController;
    ToolsCharacterController toolsCharacter;
    InventoryController inventoryController;
    ToolBarController toolbarController;
    Rigidbody2D movement; 

    private void Awake()
    {
        characterController = GetComponent<PlayerController>();
        toolsCharacter = GetComponent<ToolsCharacterController>();
        inventoryController = GetComponent<InventoryController>();
        toolbarController = GetComponent<ToolBarController>();
        movement = GetComponent<Rigidbody2D>();
    }

    public void DisableControl()
    {
       //Debug.Log("disable being called from disable class");
        characterController.enabled = false;
        toolsCharacter.enabled = false;
        inventoryController.enabled = false;
        toolbarController.enabled = false;
        movement.velocity = Vector2.zero;
        movement.angularVelocity = 0f;

     
    }

    public void EnableControl()
    {
        //Debug.Log("enabling player");
        characterController.enabled = true;
        toolsCharacter.enabled = true;
        inventoryController.enabled = true;
        toolbarController.enabled = true;
    }
}
