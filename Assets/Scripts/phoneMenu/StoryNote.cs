using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNote : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI NoteText;
    [SerializeField] TMPro.TextMeshProUGUI NameText;
    [SerializeField] GameObject messageicon;
    [SerializeField] GameObject noteicon;
    [SerializeField] GameObject callicon;
    [SerializeField] GameObject noteText;
    [SerializeField] GameObject nameText;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject backButton; 
    public List<StoryProgress> storyProgress = new List<StoryProgress>();
    public List<NPCDefintition> npcDefintition = new List<NPCDefintition>();
    public List<int> npcsLoreLevel = new List<int>();
    int currentPage=0; 
    private void Awake()
    {
        //storyProgress = GetComponent<StoryProgress>();
        //npcDefintition = GetComponent<NPCDefintition>();
        if(storyProgress == null)
        {
            Debug.Log("story progress is null");
        }
        if(npcDefintition == null)
        {
            Debug.Log("npc is null");   
        }
    }
    public void ButtonPressed()
    {
        messageicon.SetActive(false);
        noteicon.SetActive(false);
        callicon.SetActive(false);
        exitButton.SetActive(true);
        backButton.SetActive(true);
        nextButton.SetActive(true);
        noteText.SetActive(true);
        nameText.SetActive(true);
        GetLoreLevel();
        GetQuestProgress();

    }
    public void GetLoreLevel()
    {
        npcsLoreLevel.Clear();

        if (npcDefintition != null)
        {
            foreach (var npc in npcDefintition)
            {
                //Debug.Log($"{npc.DailyData.loreLevel}");
                npcsLoreLevel.Add(npc.DailyData.loreLevel);
            }
        }
        else
        {
            Debug.Log("npcdef is null");
        }
            
    }
    public void GetQuestProgress()
    {
        
        if (storyProgress != null)
        {   NameText.text = storyProgress[currentPage].Name;
            NoteText.text = ""; // Clear the text first

            for (int i = 0; i <= npcsLoreLevel[currentPage]; i++) // Fix the loop condition
            {
                if (i < storyProgress[currentPage].storyline.Count)
                {
                    NoteText.text += storyProgress[currentPage].storyline[i].text + "\n"; // Append each line with a newline
                }
                
            }
            nextButton.SetActive(currentPage < storyProgress.Count - 1);
            backButton.SetActive(currentPage > 0);

        }
        else
        {
            Debug.Log("story progress is null");
        }
       
    }
    public void ExitButtonPressed()
    {
        noteText.SetActive(false);
        messageicon.SetActive(true);
        noteicon.SetActive(true);
        callicon.SetActive(true);
        exitButton.SetActive(false);
        nextButton.SetActive(false);
        nameText.SetActive(false);
        backButton.SetActive(false);
    }
    public void BackButtonPressed()
    {
       
        currentPage--; 
        GetQuestProgress();
    }
    public void NextButtonPressed()
    {
        
        currentPage++;
        GetQuestProgress();
    }

}

