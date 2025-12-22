using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialogue System/Dialogue")]
public class DialogueContainer : ScriptableObject
{

    public enum DialogueType { normal, quest}
    public DialogueType dialogueType;
    public string StoryItemTrigger;
    public List<DialogueLines> line;
    public Actor actor;
    public bool givePresent;
    public Item GiftObject;
    public List<DialoguesChoices> choices;

}
[System.Serializable]
public class DialogueLines
{
    public string text;
    public Texture2D expression;
   // public Sprite dessert;
}

[System.Serializable]
public class DialoguesChoices
{
    public string ChoiceText;
    public DialogueContainer dialogueContainer;
    public bool isQuest;
    public float score = 0.1f;
}
