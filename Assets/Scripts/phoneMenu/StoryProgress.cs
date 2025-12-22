using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StoryProgress")]
public class StoryProgress : ScriptableObject
{
    public List<StoryLines> storyline;
    public string Name; 
    public Actor actor;
 
}
[System.Serializable]
public class StoryLines
{
    public string text;
}