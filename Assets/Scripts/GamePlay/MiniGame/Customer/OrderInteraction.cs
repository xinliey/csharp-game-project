using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInteraction : Interactable
{
    private Character character;
    [SerializeField] PlayerScoreRecord data;
    DessertMenuTrigger menu;
    private BoxCollider2D boxCollider;
    private bool isOpened = false;
    private void Awake()
    {
        character = GetComponent<Character>();
        menu = GetComponent<DessertMenuTrigger>();
        boxCollider = GetComponent<BoxCollider2D>();
     
    }
    public override void Interact(Character character)
    {
        if (isOpened == true)
        {
            menu.CloseTab();
            isOpened = false;
        }
        else if (isOpened == false)
        {
            if (data.MenuLooked == true)
            {
                if (data.ordered == true)
                {
                    menu.DisplayOrder();
                    isOpened = true;

                }
                else
                {
                    DisableJay();
                }
                
                
            }
            else
            {
                menu.DisplayMenu();
               //Debug.Log("jay will show you today's menu ");
                data.MenuLooked = true;
                isOpened = true;
            }

        }
        
        

    }
    public void DisableJay()
    {
        boxCollider.enabled = false;

    }
    public void EnabledJay()
    {
        boxCollider.enabled = true;
    }
}
