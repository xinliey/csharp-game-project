using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager != null)
            {
                StartCoroutine(dialogueManager.ShowDialog(dialogue));
            }
        }
    }
}
