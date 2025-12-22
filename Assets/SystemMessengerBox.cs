using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMessengerBox : MonoBehaviour
{
    public static SystemMessengerBox Instance { get; private set; }
    [SerializeField] GameObject messageBoxUI;
    [SerializeField] private TMPro.TextMeshProUGUI messageText;
    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercent;
    float timePerLetter = 0.05f;
    private string lineToShow;
    private float totalTimeToType;
    private float currentTime;
    private bool isTyping = false;
    DisableControls disableControls;
    //public List<string> lines;
    int currentLine; 
    private void Awake()
    {
        messageBoxUI.SetActive(false);
        Instance = this;
        //since disable is in another gameobject but still in same scene
        GameObject player = GameObject.FindWithTag("Player"); // Ensure your player has the "Player" tag
        if (player != null)
        {
            disableControls = player.GetComponent<DisableControls>();
           
        }
        
    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Z) && isTyping)
        {
            visibleTextPercent = 1f;
            UpdateText();
            isTyping = false;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            HideMessage();
        }

    }

  
    public void ShowMessage(string message)
    {
        lineToShow = message;
        messageBoxUI.SetActive(true);
        StartCoroutine(TypeOutMessage());
        
    }
    private IEnumerator TypeOutMessage()
    {
        
        isTyping = true;
        disableControls.DisableControl();
        messageText.text = "";
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;

        while(visibleTextPercent < 1f)
        {
            currentTime += Time.deltaTime;
            visibleTextPercent = currentTime / totalTimeToType;
            visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0f, 1f);
            UpdateText();
            yield return null;
        }
        isTyping = false;
    }
    private void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercent);
        messageText.text = lineToShow.Substring(0, letterCount);
    }
    private void HideMessage()
    {
        messageBoxUI.SetActive(false);
        isTyping = false;
        disableControls.EnableControl();
    }
    
}
