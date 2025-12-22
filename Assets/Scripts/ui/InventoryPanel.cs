using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryPanel : ItemPanel

{

    public override void OnClick(int id)
    {
        Debug.Log("you click on the inventory");
        GameManager.instance.dragAndDropManager.OnClick(inventory.slots[id]);
        Show(); 

    }
    public override void RightClick(int id)
    {
       
        GameManager.instance.dragAndDropManager.RightClick(inventory.slots[id]);
        Show();
    }
}
