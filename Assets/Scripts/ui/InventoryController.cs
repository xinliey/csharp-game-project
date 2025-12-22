using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject statusPanel;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (panel.activeInHierarchy == false)
            {
                Open();
            }
            else
            {
                Close();
            }
           
        }
    }
    public void Open() {
        //update toolbar when update inventory by turning off toolbar when updating inventory 
        panel.SetActive(true);
        statusPanel.SetActive(false); 
        toolbarPanel.SetActive(false);
        storePanel.SetActive(false);
    }
    public void Close()
    {
        panel.SetActive(false);
        statusPanel.SetActive(true);
        toolbarPanel.SetActive(true);
        storePanel.SetActive(false);
    }
}
