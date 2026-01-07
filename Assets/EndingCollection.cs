using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class EndingImage {
    public string name;
    public Sprite icon;
    
}


public class EndingCollection : MonoBehaviour
{
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] List<Image> buttonToStoreImage = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> TextInsideButton = new List<TextMeshProUGUI>();
    public List<EndingImage> endingavailable = new List<EndingImage>();
    
    int ButtonOrder = 0;
    public void Awake()
    {
        foreach(CutSceneDialogue n in player.ending)
        {
            Debug.Log($"{n.NextTriggerName}");
            for (int i = 0; i < endingavailable.Count; i++)
            {
                if (n.NextTriggerName == endingavailable[i].name)
                {
                    buttonToStoreImage[ButtonOrder].sprite = endingavailable[i].icon;
                    TextInsideButton[ButtonOrder].text = endingavailable[i].name;
                    ButtonOrder += 1;
                }
            }
            
        }
    }

}
