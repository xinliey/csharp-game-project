using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController characterController;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    Character character; //i dont know what is this

    private void Awake()
    {
        characterController = GetComponent<PlayerController>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Interact();
        }
    }

    public void Interact()
    {
        Vector2 position = rgbd2d.position + characterController.lastMovingVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            //Debug.Log($"Checking collider: {c.name}");
            Interactable loot = c.GetComponent<Interactable>();
            if (loot != null)
            {
                //Debug.Log($"Found loot item: {loot.name}");
                loot.Interact(character);
                //Debug.Log($"Successfully looting");
                break;
            }

        }
    }
}
