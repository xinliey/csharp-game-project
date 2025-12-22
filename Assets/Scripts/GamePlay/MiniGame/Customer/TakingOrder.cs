using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakingOrder : Interactable
{
    private Character character;
    [SerializeField] CustomerNPC customer;
    [SerializeField] GameObject orderpanel;
    [SerializeField] Image dessertImage;
    [SerializeField] Text orderText;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] AudioSource CollectingMoney;
    [SerializeField] GameObject plate;
    [SerializeField] AudioSource wrongserving;
    public string currentDessert;
    bool panelOpened = false;
    private void Awake()
    {
       
        orderpanel.SetActive(false);
        character = GetComponent<Character>();
        customer.data.Ordered = false;
    }
    public override void Interact(Character character)
    {
        Trading trading = character.GetComponent<Trading>();
        if (trading == null) { return; }
        if (customer.data.Ordered == false&&player.ordered==false) //new customer wants to order and player already sent the previous order to jay 
        {
            if (panelOpened == true)
            {
                orderpanel.SetActive(false);
                //Debug.Log($"current dessert is {currentDessert}");
                
            }
            else
            {
                int i = Random.Range(0, customer.OrderDialogue.Count);
                DisplayOrder(i);

            }
            player.ordered = true;
        }
        else if(customer.data.Ordered == true && player.isDessertInHand==true) 
        {

            if (currentDessert == player.dessertHolding)
            {
                SystemMessengerBox.Instance.ShowMessage("Thank you, here is the money");
                plate.SetActive(true);
                CollectingMoney.Play();
                if (player.correct == true)
                {
                    trading.PartTime(this);
                }
                else
                {
                    trading.PartTimeWrong(this);
                }

                player.isDessertInHand = false;
                player.dessertHolding = null;
            }
            else
            {
                wrongserving.Play();
                orderpanel.SetActive(false);
            }
            
        }else if (customer.data.Ordered == false && player.ordered == true) //player try to take new order without sending the previous order to jay
        {
            SystemMessengerBox.Instance.ShowMessage("I should deliver the order to Jay first");
        }
        else
        {
            orderpanel.SetActive(false);
        }    
        
    }
    public void DisplayOrder(int i)
    {
        orderpanel.SetActive(true);
        panelOpened = true;
        dessertImage.sprite = customer.OrderDialogue[i].dessert; 
        orderText.text = customer.OrderDialogue[i].dialogue;
        currentDessert = customer.OrderDialogue[i].DessertName;
        player.currentDessert = currentDessert;
        customer.data.Ordered = true;
    }


}
