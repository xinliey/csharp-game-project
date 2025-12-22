using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Dialogue System/Messenger")]
public class MessengerContainer : ScriptableObject
{
    public List<MessengerLines> line;
    public List<MessengerChoices> choices;
}
[System.Serializable]
public class MessengerLines
{
    public string text;
    public string name;
    public Sprite profile;
    public bool playerNext;
}
[System.Serializable]
public class MessengerChoices
{
    public string ChoiceText;
    public MessengerContainer messengerContainer;
    public float score = 0.1f;
    public NPCDefintition npc;
}

