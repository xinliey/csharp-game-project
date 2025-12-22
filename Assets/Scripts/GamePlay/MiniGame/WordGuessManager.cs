using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WordGuessManager : MonoBehaviour
{
    [SerializeField] GameObject hintbtn;
    [SerializeField] GameObject Nextbtn;
    [SerializeField] GameObject Quitbtn;
    [SerializeField] GameObject hintfunction;
    [SerializeField] GameObject AlphabetChoices;
    [SerializeField] GameObject AlphabetAnswer;
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] GameObject CompletedPanel;
    [SerializeField] TMPro.TextMeshProUGUI gamecompleteText;
    [SerializeField] TMPro.TextMeshProUGUI score;
    [SerializeField] TMPro.TextMeshProUGUI hint;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] AudioSource wrongClick;
    [SerializeField] Vector3 target;
   
    public AudioSource hintSound;
    public AudioSource pickupPhone;
    public GridLayoutGroup gridLayout;
    public Transform letterChoices;
    public string answer = "Jay";
    public List<Image> HpLevel = new List<Image>();
    public List<Button> choiceButtonList = new List<Button>();
    public List<Button> answerButtonList = new List<Button>();
    public List<MinigameWords> wordAnswer = new List<MinigameWords>();
    public string currentAnswer = "";
    MinigameWords currentWord;
    int currentscore;
    int mistake = 0;
    int dailyAttempt = 3;
    //string nameEssentialScene = "essential";
    string nameNewGameStartScene = "schoo_hall";

    private void Awake()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        while (GameManager.instance == null)
            yield return null; // wait one frame

        GameManager.instance.DisableFade();
        GameManager.instance.minigameDisable();
    }
    public void HintBtnClicked()
    {
        GameManager.instance.ClickButtonSound();
        StartCoroutine(PlaySoundsAndStartDialogue());
    }

    private IEnumerator PlaySoundsAndStartDialogue()
    {

        // Play first sound
        hintSound.Play();
        yield return new WaitWhile(() => hintSound.isPlaying);
        dialogueSystem = hintfunction.GetComponent<DialogueSystem>();
        dialogueSystem.Initialize(currentWord.dialogueContainer);
       hintfunction.SetActive(true);
        // Play pickup sound
        pickupPhone.Play();
        yield return new WaitWhile(() => pickupPhone.isPlaying);
    

        
        



    }
    public void NextButtonClicked()
    {
        GameManager.instance.ClickButtonSound();
        for (int i = 0; i < answerButtonList.Count; i++)
        {
            TMP_Text tmpText = answerButtonList[i].GetComponentInChildren<TMP_Text>(); // Use answerButtonList
            if (tmpText != null)
            {
                tmpText.text = ""; // Clear the text
            }
            answerButtonList[i].gameObject.SetActive(false); // Hide buttons
        }
        currentAnswer = "";
        Start();
    }
    private void Start()
    {
        CompletedPanel.SetActive(false);
        GenerateLetterAnswer();
        GenerateLetterChoice();

    }
    private void GenerateLetterAnswer()
    {
        List<MinigameWords> availableWords = wordAnswer.FindAll(word => !word.wordUsed);
        if (availableWords.Count > 0) // Ensure there are available words
        {
            currentWord = availableWords[UnityEngine.Random.Range(0, availableWords.Count)];
            answer = currentWord.word.ToUpper();
            hint.text = currentWord.Hint;
        }
        else
        {
            Debug.Log("resetting word used ");
            foreach(MinigameWords word in wordAnswer)
            {
                word.wordUsed = false;
            }
            GenerateLetterAnswer();
        }

        for (int i = 0; i < answer.Length; i++)
        {
            answerButtonList[i].gameObject.SetActive(true);
        }
        int totalWidth = (int)gridLayout.cellSize.x * answer.Length;
        int containerWidth = (int)((RectTransform)gridLayout.transform).rect.width;

        int leftPadding = (containerWidth - totalWidth) / 2;
        gridLayout.padding.left = Mathf.Max(leftPadding, 0);
    }
    private void GenerateLetterChoice()
    {
        //pick letters for choices including the answer letters
        HashSet<char> letterChoicesSet = new HashSet<char>(answer);

        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        while (letterChoicesSet.Count < 14)
        {
            char randomLetter = letters[UnityEngine.Random.Range(0, letters.Length)];

            if (!letterChoicesSet.Contains(randomLetter) && !answer.Contains(randomLetter.ToString().ToUpper()))
            {
                letterChoicesSet.Add(randomLetter);
            }
        }
        List<char> letterChoices = new List<char>(letterChoicesSet);
        //randomize the order
        for (int i = 0; i < letterChoices.Count; i++)
        {
            char temp = letterChoices[i];
            int randomIndex = UnityEngine.Random.Range(0, letterChoices.Count);
            letterChoices[i] = letterChoices[randomIndex];
            letterChoices[randomIndex] = temp;
        }
        //put into the choice button    
        for (int i = 0; i < choiceButtonList.Count && i < letterChoices.Count; i++)
        {
            TMP_Text tmpText = choiceButtonList[i].GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
            {
                char letter = letterChoices[i];

                tmpText.text = letterChoices[i].ToString();
                choiceButtonList[i].onClick.RemoveAllListeners();  // Clear previous listeners
                choiceButtonList[i].onClick.AddListener(() => OnLetterClicked(letter.ToString()));
            }
        }
    }

    private void OnLetterClicked(string letter)
    {
        GameManager.instance.ClickButtonSound();
        if (currentAnswer.Length < answer.Length)
        {
            if (answer.Contains(letter))
            {


                for (int i = 0; i < answer.Length; i++)
                {
                    if (answer[i] == letter[0]) // Comparing char with string letter[0]
                    {
                        currentAnswer += letter;
                        TMP_Text tmpText = answerButtonList[i].GetComponentInChildren<TMP_Text>();
                        if (tmpText != null)
                        {
                            tmpText.text = letter.ToString();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("wrong letter");
                wrongClick.Play();
                HpLevel[mistake].gameObject.SetActive(false);
                mistake += 1;
                if (mistake == HpLevel.Count)
                {
                    CompletedPanel.SetActive(true);
                    gamecompleteText.text = "GameOver";
                    Quitbtn.gameObject.SetActive(true);
                    player.finishedSchool = true;
                }
            }
        }
        //answerButtonList[].GetComponentInChildren<Text>().text = letter;

        if (currentAnswer.Length == answer.Length)
        {
            CheckAnswer();

        }

    }
    private void CheckAnswer()
    {
        currentWord.wordUsed = true;
        currentscore += 1;
        player.wordguessScore += 1;
        score.text = currentscore.ToString();
        dailyAttempt -= 1;
        //Debug.Log($"{dailyAttempt}round left to go");
        CompletedPanel.SetActive(true);
        Nextbtn.gameObject.SetActive(true);
        Quitbtn.gameObject.SetActive(false);
        if (dailyAttempt == 0)
        {
            Nextbtn.gameObject.SetActive(false);
            Quitbtn.gameObject.SetActive(true);
            //Debug.Log("you have completed today's school quest");
            player.finishedSchool = true;
        }

    }

    public void QuitbtnClicked()
    {
        GameManager.instance.ClickButtonSound();
        GameManager.instance.minigameEnable();
        SceneTransitionManager.Instance.InitSwitchScene(nameNewGameStartScene, target);
        player.InMiniGameScene = false;
        if (player.finishedSchool == true)
        {
            GameManager.instance.gameTime.SkipSchoolTime();
           
        }
    }
   
}
