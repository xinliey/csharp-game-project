using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] ToolBarController toolbarController;
  

    private void Start()
    {
        Init(); 
        toolbarController.onChange += Highlight;
        //initialize by highlighting the first item
        Highlight(0);
    }

    public override void OnClick(int id) {
        toolbarController.Set(id);
        Highlight(id);

    }
    public int currentSelectedTool;
    public void Highlight(int id)
    {
        //unhighlight the previous tool to highlight the next tool
        buttons[currentSelectedTool].Highlight(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].Highlight(true);
    }
}
