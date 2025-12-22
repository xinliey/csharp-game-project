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

    //to absorb the item that drop
    public void Add(Item item,int count =1)
    {
        if(item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if(itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {// add non stackabke item into inventory 
                itemSlot = slots.Find(x => x.item == null);
                if(itemSlot!= null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count; 
                    
                }
            }
        }else
        {//add non stackable item to our inventory
            ItemSlot itemSlot =slots.Find(x => x.item == null);
            if(itemSlot != null)
            {
                itemSlot.item = item;
                itemSlot.count = count;
            }
        }

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
