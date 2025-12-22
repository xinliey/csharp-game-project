using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[CreateAssetMenu(menuName = "Data/Minigame/WordGuess")]
public class MinigameWords : ScriptableObject 
{
    public string word = "";
    public DialogueContainer dialogueContainer;
    public string Hint; 
    public bool wordUsed;
}
