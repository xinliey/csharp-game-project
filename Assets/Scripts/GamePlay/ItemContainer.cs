using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;
    
    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }
    //set work with the replacing item
    public void Set(Item item , int count)
    {
            this.item = item;
            this.count = count;

    }
    public void Clear()
    {
    item = null;
    count = 0;

    }
    //set the item in the slot 
    
}
//adding copy and clear ability into the item slot 

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;
    public bool inventoryFull; 
    //to absorb the item that drop
    public void Add(Item item,int count =1)
    {
        if(item.stackable == true)
        {
            //existed stackable item
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if(itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {// new stackable
               // CheckInventorySpace();
               
                    itemSlot = slots.Find(x => x.item == null);
                    if (itemSlot != null)
                    {
                    inventoryFull = false;
                        itemSlot.item = item;
                        itemSlot.count = count;

                    }
                    else
                    {
                        SystemMessengerBox.Instance.ShowMessage("Inventory is full, please sell or drop item");
                    inventoryFull = true;
                    }

                
               
                
            }
        }else
        {//add non stackable item to our inventory
           // CheckInventorySpace();
           
                ItemSlot itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                inventoryFull = false;
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
                else
                {
                    SystemMessengerBox.Instance.ShowMessage("Inventory is full, please sell or drop item");
                inventoryFull = true;
                }
            
            
        }

    }
    public void AddStockToShop(Item item, int count = 1)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == null); //stocking product to the empty space 
        if (itemSlot != null)
        {
            itemSlot.item = item;
            itemSlot.count = count;
        }
   
    }
    public bool CanAdd(Item item, int count = 1)
    {
        if (item.stackable)
        {
            // If stack exists, we can add
            ItemSlot existingSlot = slots.Find(x => x.item == item);
            if (existingSlot != null)
                return true;
        }

        // Otherwise need empty slot
        ItemSlot emptySlot = slots.Find(x => x.item == null);

        return emptySlot != null;
    }
    public void Remove(Item item , int count = 1)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == item);
        if (itemSlot != null)
        {
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
                isDirty = true;
            }
        }
        
    }
    public bool isDirty = false;

    public bool IsDirty()
    {
        return isDirty;
    }

    public void SetClean()
    {
        isDirty = false;
    }
}
