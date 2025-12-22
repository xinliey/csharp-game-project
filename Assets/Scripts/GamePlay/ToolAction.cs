using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public int energCost = 0; 
    //this script determine how much energy lost for each action
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.Log("onapply is not implemented");
        return true; 
    }
    public virtual void OnItemUsed(Item usedItem, ItemContainer inventory)
    {

    }
}
