 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 2;
    
    public int selectedTool;

    public Action<int> onChange;
    private void Update()
    {
        //to scroll to change selected item
        float delta = Input.mouseScrollDelta.y;
        if(delta != 0)
        {
            if (delta > 0)
            {
                selectedTool += 1;
                 //make sure the selected tool is inside the limit of the toolbelt
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }
            else
            {
                selectedTool -= 1;
                selectedTool = (selectedTool <= 0 ? toolbarSize - 1: selectedTool); 
            }
            onChange?.Invoke(selectedTool);
        }
    }
    internal void Set(int id)
    {
        selectedTool = id;
    }
}
