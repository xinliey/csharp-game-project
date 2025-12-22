using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingDessert : Interactable
{
    private Character character;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField]OrderInteraction order; 
    [SerializeField] PlayerScoreRecord data;
    [SerializeField] AudioSource clankingsound;
    private void Awake()
    {
        character = GetComponent<Character>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
      
    }
    public override void Interact(Character character)
    {
        if (data.isDessertInHand == false)
        {
            clankingsound.Play();
            DessertTaken();
            data.isDessertInHand = true;
            Debug.Log("food taken by you");
            data.dessertHolding = data.currentCooking;
            //data.ordered = false;
            order.EnabledJay();

        }
        else
        {
            SystemMessengerBox.Instance.ShowMessage("you can only carry one dessert at one time");

        }

    }

    public void PreparingDessert()
    {
        Debug.Log("preparing food");
        StartCoroutine(WaitForDessert());

    }
    private IEnumerator WaitForDessert()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }
    private void DessertTaken()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }
}
