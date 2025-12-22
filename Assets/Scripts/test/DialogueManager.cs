using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] Image expressionImage; // Image to display NPC expressions
    [SerializeField] int lettersPerSecond = 20;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogueManager Instance { get; private set; }

    private Dialogue dialogue;
    private int currentLine = 0;
    private bool isTyping;
    private bool isDialogueActive;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isDialogueActive)
        {
            HandleUpdate();
        }
    }

    public IEnumerator ShowDialog(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.lines.Length == 0)
        {
            Debug.LogError("Dialogue is null or has no lines.");
            yield break;
        }

        OnShowDialog?.Invoke();
        this.dialogue = dialogue;
        dialogBox.SetActive(true);
        isDialogueActive = true;
        currentLine = 0;

        yield return TypeDialog(dialogue.lines[currentLine].text);
        UpdateExpression(dialogue.lines[currentLine].npcExpression);
    }

    private void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            currentLine++;

            if (currentLine < dialogue.lines.Length)
            {
                StartCoroutine(TypeDialog(dialogue.lines[currentLine].text));
                UpdateExpression(dialogue.lines[currentLine].npcExpression);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }

    private void UpdateExpression(Sprite expression)
    {
        if (expression != null)
        {
            expressionImage.sprite = expression;
            expressionImage.gameObject.SetActive(true);
        }
        else
        {
            expressionImage.gameObject.SetActive(false);
        }
    }

    private void EndDialogue()
    {
        dialogBox.SetActive(false);
        isDialogueActive = false;
        OnHideDialog?.Invoke();
    }
}
