    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    [SerializeField] GameObject toolbarPanel;
    DessertMenuTrigger menuaTrigger;
    InventoryController inventory;

    private void Awake()
    {
        inventory = GetComponent<InventoryController>(); // Get the script on the same GameObject
    }
    public void BeginMiniGame(DessertMenuTrigger menuTrigger)
    {
        this.menuaTrigger = menuTrigger;

        toolbarPanel.SetActive(false);
        if (inventory != null)
        {
            inventory.enabled = false;
        }
       
    }
    
}
