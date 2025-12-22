using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorePanel : ItemPanel
{
    [SerializeField] Trading trading;
    public override void OnClick(int id)
    {
        if(GameManager.instance.dragAndDropManager.itemSlot.item == null)
        {
            //check if player is holding any item 
            BuyItem(id);
        }
        else
        {
            SellItem();

        }
        
        Show();
    }

   private void SellItem()
    {
      
        trading.SellItem();
    }
   private void BuyItem(int id)
   {
        trading.BuyItem(id);

    }
}
