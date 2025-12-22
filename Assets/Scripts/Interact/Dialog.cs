using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogLine
{
    public string text;          // Text of the dialog line
    public Sprite expression;    // NPC expression associated with the dialog line
}

[System.Serializable]
public class Dialog
{
    [SerializeField] List<DialogLine> lines; // List of dialog lines
    public List<DialogLine> Lines
    {
        get { return lines; }
    }
}
