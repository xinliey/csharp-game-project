using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;
[Serializable]
public class CutSceneTalk
{
    public Texture2D npc;
    public bool ChangeImage;
    public Sprite background; 
    public string line;
    public bool noBackground;
}
[Serializable]
public class OptionCutscene
{
    public string Option;
    public float Score;
    
    public CutSceneDialogue OptionDialogue;
}
[CreateAssetMenu(menuName = "Data/CutSceneDialogue")]
public class CutSceneDialogue : ScriptableObject
{
    public string cutscenename;
    public bool Selectable;
    public int cutSceneLevel; 
    public bool nextTrigger;
    public string NextTriggerName;
    public bool EndingCutscene;
   // public string NewScene;
    //public Vector3 location; 
    public List<CutSceneTalk> dialogue;
    public List<OptionCutscene> options; 

}
