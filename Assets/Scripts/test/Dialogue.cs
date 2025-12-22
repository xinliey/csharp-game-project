using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string npcName;
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 5)] public string text; // The line of dialogue
    public Sprite npcExpression;        // Optional: Expression changes
}
