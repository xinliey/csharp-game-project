using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsCharacterController : MonoBehaviour //collecting item from boxes
{
    PlayerController characterController2d;
    //Character character;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
  
    public void Awake()
    {
        //character = GetComponent<Character>();
        characterController2d = GetComponent<PlayerController>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseTool();
        }
    }

    public void UseTool()
    {
        Vector2 position = rgbd2d.position + characterController2d.lastMovingVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            //Debug.Log($"Checking collider: {c.name}");
            LootItem loot = c.GetComponent<LootItem>();
            if (loot != null)
            {
                //Debug.Log($"Found loot item: {loot.name}");
                loot.Loot();
                //Debug.Log($"Successfully looting");
                break;
            }
           
        }
    }

    
}
