using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> customers;
    [SerializeField] DialogueContainer dialog;
    private List<int> usedCustomers= new List<int>();
    [SerializeField] GameObject ExitCafe;
    [SerializeField] AudioSource customerenter;
    [SerializeField] PlayerScoreRecord player;
    int CustomerServed = 0;
    //private bool NextCustomer = false;
    private void Awake()
    {
        ExitCafe.SetActive(false);
        foreach(GameObject customer in customers)
        {
            customer.SetActive(false);
        }
        ReleaseCustomer();
    }

    public void ReleaseCustomer()
    {
        if (usedCustomers.Count < customers.Count)
        {
            StartCoroutine(WaitForRelease());
        }
        
    }

    private IEnumerator WaitForRelease()
    {
       
        yield return new WaitForSeconds(Random.Range(5f, 30f));

        // If there are still customers that have not been used
        if (usedCustomers.Count < customers.Count)
        {
            int i;
            do
            {
                // Generate a random number between 0 and the number of customers
                i = Random.Range(0, customers.Count);
            } while (usedCustomers.Contains(i));  // Ensure it's not already used

            
            usedCustomers.Add(i);
            customerenter.Play();
            customers[i].SetActive(true);
        }
        ReleaseCustomer();
    
        
    }

    public void CheckOrdered()
    {
        CustomerServed += 1;
        //Debug.Log($"current serving is {CustomerServed}");
        if (CustomerServed == 4)
        {
            GameManager.instance.dialogueSystem.Initialize(dialog);
            player.FirstOrder = true;

        }
       player.inPartTimeScene = false;
        ExitCafe.SetActive(true);
        
    }

}
