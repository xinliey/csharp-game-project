using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diarytable : Interactable
{
    [SerializeField] GameObject spark;
    CutSceneManager cutsceneManager;
    [SerializeField] PlayerScoreRecord player;
    [SerializeField] AudioSource notifSound;

    private void Awake()
    {
        if (player.gameDay==0)
        {
            Debug.Log("enabling spark , game dat is 0");
            spark.gameObject.SetActive(true) ;
        }
        else
        {
            spark.gameObject.SetActive(false);
        }
        if (player.finishedSchool == true)
        {
            
            if (player.gameDay % 3 == 0)
            {
                Debug.Log("gameday is enough to trigger messenger");
                if (player.TodayTexted == false) //finished school today but hasn't do the texting. 
                {
                    TriggerMessenger();
                }
            }

        }
    }
    public override void Interact(Character character)
    {
        if (player.gameDay==0)
        {
            GameManager.instance.cutSceneManager.Initialize(player.quest[1].cutscene);
            spark.SetActive(false);
            player.quest[1].isDone = true;
            player.gameDay = 1;
        }
        
    }
    public void TriggerMessenger()
    {
      StartCoroutine(SendingSignal());
    }
    private IEnumerator SendingSignal()
    {
        yield return new WaitForSeconds(5f);
        notifSound.Play();
        player.MessengerTrigger = true;
        SystemMessengerBox.Instance.ShowMessage("new message notification on your phone");

    }
}

