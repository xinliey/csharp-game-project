using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
    public GameObject itemPrefab;
    public int price = 20;
    public bool canBeSold = true;
    public bool isQuestItem;
    public bool isLookable; //player can press a button to look detail of the item
    public string ItemData;
}
