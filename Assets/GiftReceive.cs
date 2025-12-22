using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftReceive : MonoBehaviour
{
    NPCCharacter npcCharacter;
    NPCDefintition npcDefintition;
    TalkInteract talkInteract;
    ToolBarController toolBarController;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject toolBarObject;
    [SerializeField] ItemContainer playerInventory;
    [SerializeField] ItemPanel inventoryItemPanel;
    Item selectedItem;
   
    Gifting gifting; 
    private void Awake()
    {
        
        toolBarController = GetComponent<ToolBarController>();
        //npc data will be send from gifting scritps 
       
    }
    public void GiftingProcess(Gifting gifting)
    {
        this.gifting = gifting;
        GetSelectedItem();
    }
    public void GetCurrentNPCData(NPCCharacter npc)
    {
        npcCharacter = npc;
        npcDefintition = npcCharacter.character;
        
        
    }
    public void GetSelectedItem()
    {
        int selectedIndex = toolBarController.selectedTool;
        //Debug.Log($"index is {selectedIndex}");
        if (selectedIndex >= 0 && selectedIndex < playerInventory.slots.Count)
        {
            selectedItem = playerInventory.slots[selectedIndex].item;
            inventoryItemPanel.Show();
            //Debug.Log($"Selected Item: {selectedItem.Name}"); //the unhiglight is selected one
            if (npcDefintition != null)
            {
                if (npcCharacter.GiftPresent == false) //prevent double gifting 
                {
                    playerInventory.Remove(selectedItem,1);
                    if (npcDefintition.itemLike.Contains(selectedItem))
                    {
                        //Debug.Log($"{npcDefintition.Name} likes this item!");
                        npcCharacter.IncreaseRS(0.05f);
                        talkInteract.LikeGiftDialogue();

                    }
                    else if (npcDefintition.itemHate.Contains(selectedItem))
                    {
                        //Debug.Log($"{npcDefintition.Name} hates this item!");
                        npcCharacter.IncreaseRS(-0.05f);
                        talkInteract.HateGiftDialogue();
                    }
                    else
                    {
                        // Debug.Log($"{npcDefintition.Name} has no opinion on this item.");
                        talkInteract.NormalGiftDialogue();

                    }
                    npcCharacter.GiftPresent = true;
                    
                }
                else
                {
                    SystemMessengerBox.Instance.ShowMessage("you given npc gift already");
                  
                }
            }
            
        }
    }

    public void GiftingDialogue(TalkInteract talk)
    {
        talkInteract = talk;
        
    }
    
}