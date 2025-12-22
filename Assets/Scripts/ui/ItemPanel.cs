using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] protected ItemContainer inventory;
    public List<InventoryButton> buttons;
    //private bool isDirty = false; //chat 
    private void Start()
    {
        Init();
        
    }
    public void Init()
    {
        SetSourcePanel();
        SetIndex();
        Show();
    }
    private void SetSourcePanel()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetItemPanel(this);
            //Debug.Log($"SetItemPanel called for {buttons[i].gameObject.name}");
        }
    }
    private void OnEnable()
    {
        Clear();
        Show();
    }
    private void LateUpdate()
    {
        if(inventory == null) { return; }
        if (inventory.isDirty)
        {
            Show();  // Update the UI by chat
            //inventory.SetClean();//chat
            inventory.isDirty = false;
        }
    }
    public void SetIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }
    //if there's no item call clean mothod to the button
    public void Show()
    {
        if (inventory == null) { return; }
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
    //clearing all the sloth only left with one with the item to sell 
    public void Clear()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clean();
        }
    }
    //bringing in the item available for sell 
    public void SetInventory(ItemContainer newInventory)
    {
        inventory = newInventory;
    }
    public virtual void OnClick(int id)
    {
          
    } 

    public virtual void RightClick(int id)
    {

    }
}
