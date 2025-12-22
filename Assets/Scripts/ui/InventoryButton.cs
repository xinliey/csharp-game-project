
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text text;
    [SerializeField] Image highlight;
    public bool ForSale;
    int myIndex;
    ItemPanel itemPanel;
    public void SetIndex(int index) //the variable to count amount of the item
    {
        myIndex = index;
    }
    public void SetItemPanel(ItemPanel source) //assign the right panel to the itempanel
    {
        itemPanel = source;
        //Debug.Log($"ItemPanel asspkigned to {gameObject.name}");
    }

    public void Set(ItemSlot slot)//putting item into the box 
    {
       
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon; 
        
        if(slot.item.stackable == true&&ForSale==false)//for normal inventory box
        {
            text.gameObject.SetActive(true); //setting the game icon active and show in the inventory button 
            text.text = slot.count.ToString();

        }else if (ForSale == true) //for inventory box in the store
        {
            text.gameObject.SetActive(true);
            text.text = slot.item.price.ToString();
        }
        else //for other inventory that cant be sell 
        {
            text.gameObject.SetActive(false);
        }
        //if it's not stackable it will be set inactive as usual
       

    }
    //method to remove item from the inventory box 
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
    //send the itemslot with this inventory button to the drag and drop 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemPanel == null)
            {
                Debug.LogError("itemPanel is null! Ensure SetItemPanel() is called properly.");
                return;
            }

            // greg : ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
            itemPanel.OnClick(myIndex);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
           
            itemPanel.RightClick(myIndex);
        }
    }
    //highlighting the selected item 
    public void Highlight(bool b)
    {
        highlight.gameObject.SetActive(b);
    }
}
