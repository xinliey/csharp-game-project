using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject toolBarPanel;
    Store store;
    Currency money;
    TakingOrder order;
    //TableInteraction cafe;
    ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;
    [SerializeField] ItemPanel inventoryItemPanel;
    [SerializeField] PlayerScoreRecord player;
   // [SerializeField] List<Text> texts = new List<Text>();
    private void Awake()
    {
        money = GetComponent<Currency>();
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>();
    }
    public void BeginTrading(Store store)
    {
        this.store = store;
        
     
       itemStorePanel.SetInventory(store.storeContent);
        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);
        toolBarPanel.SetActive(false);
       
        /*foreach(Text n in texts)
        {
            int price = (int)(store.storeContent.slots[id].item.price * store.sellToPlayerMultip);
            n.text = price.ToString();
            id += 1;
        }
        */
    }
    internal void BuyItem(int id)
    {

        Item itemToBuy = store.storeContent.slots[id].item;
       // Debug.Log($"current item name:{itemToBuy.Name}");
        int totalPrice = (int)(itemToBuy.price);
        if (money.Check(totalPrice) == true)
        {
            //texts[id].gameObject.SetActive(false);
           
            if (itemToBuy.Name != "Potion")
            {
                money.Decrease(totalPrice);
                playerInventory.Add(itemToBuy);
                inventoryItemPanel.Show();
                store.storeContent.slots[id].count -= 1;
            }
            else
            {
                money.Decrease(totalPrice);
               
                player.maxStamina += 50;
                store.storeContent.slots[id].count -= 1;
                SystemMessengerBox.Instance.ShowMessage("your maximum stamina has increase");

            }
            
            //Debug.Log($"the remaining item in the store is{store.storeContent.slots[id].count}");
            if (store.storeContent.slots[id].count <= 0)
            {
                store.storeContent.slots[id].Clear();
            }
            
        }
    }
    public void SellItem()
    {
        //this item in draganddrop system right now can be sold or not 
        if (GameManager.instance.dragAndDropManager.CheckForSale() == true)
        {
            ItemSlot itemToSell = GameManager.instance.dragAndDropManager.itemSlot;
            int moneyGain = itemToSell.item.stackable == true ? //is item stackable
               (int)(itemToSell.item.price * itemToSell.count*store.buyFromPlayerMultip) : //if yes multiply price with quantity
               (int)(itemToSell.item.price*store.buyFromPlayerMultip); // if no , this is total price 
               money.Add(moneyGain);
            itemToSell.Clear();
           GameManager.instance.dragAndDropManager.UpdateIcon();
        }
    }

    public void StopTrading()
    { 

        store.Restocking();
        store = null;
        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        toolBarPanel.SetActive(true);
       
    }

    public void PartTime(TakingOrder order)
    {
        int moneyGain = 20;
        money.Add(moneyGain);
    }
    public void PartTimeWrong(TakingOrder order)
    {
        int moneyGain = 10;
        money.Add(moneyGain);
    }
}
