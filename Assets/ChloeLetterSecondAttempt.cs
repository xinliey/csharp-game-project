using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChloeLetterSecondAttempt : MonoBehaviour
{
    [SerializeField] GameObject letterbtn;
    [SerializeField] GameObject Panel;
    [SerializeField] TMPro.TextMeshProUGUI attempts;
    [SerializeField] GameObject confirmbtn; 
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] TMP_InputField input;
    
    int attemptremain = 3;
    public List<string> answers = new List<string>();
    private void Awake()
    {
        Panel.SetActive(false);
    }

    public void ChloeLetterBtnPressed()
    {
        GameManager.instance.gameTime.PauseTime();
        Panel.SetActive(true);
        UpdateAttempt();
       
    }                     

    public void ConfirmAnswerBtn()
    {
        string answer = (input.text).Replace(" ", "").ToLower();
        Debug.Log($"the answer input is {answer}");
        bool correct = false;
        foreach (string n in answers)
        {
            if(answer == n)
            {
                Debug.Log("correct answer");
                correct = true;
               
                break;
            }
        }
        if (correct == false)
        {
            player.chloeletterattempt += 1;
            UpdateAttempt();
           
        }
        else
        { player.currentTrigger = "HeeseungConfront";
            Panel.SetActive(false);
            letterbtn.SetActive(false);
            SystemMessengerBox.Instance.ShowMessage("what should I do, is it enough as an evidance? I need to go to him but brining someone with me should be good");
        }
    }

    private void UpdateAttempt()
    { 
        if (attemptremain - player.chloeletterattempt > 0)
        {
            attempts.text = (attemptremain - player.chloeletterattempt).ToString();

        }
        else
        {
            attempts.text = "0";
            confirmbtn.SetActive(false);
            
        }
    }

    public void HelpBtn()
    {
        Panel.SetActive(false);
        SystemMessengerBox.Instance.ShowMessage("I think I can call someone and see if they can help"); 


    }
    public void ExitBtn()
    {
        Panel.SetActive(false);
    }
}
