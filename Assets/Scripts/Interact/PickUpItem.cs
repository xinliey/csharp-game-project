using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    //[SerializeField] float ttl = 10f;
    public Item item;
    public int count = 1;
    ItemPanel toolBarInventory;

    
    private void Start()
    {
        player = GameManager.instance.player.transform;
        GameObject panelObject = GameObject.Find("ToolBarPanel");
        if (panelObject != null)
        {
            toolBarInventory = panelObject.GetComponent<ItemPanel>();
        }
        else
        {
            Debug.LogWarning("ItemPanel not found in the scene!");
        }
    }
    public void Set(Item item , int count)
    {
        this.item = item;
        this.count = count;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }

    public void Update()
    {
        if (toolBarInventory == null)
        {
            GameObject panelObject = GameObject.Find("ToolBarPanel");
            if (panelObject != null)
            {
                toolBarInventory = panelObject.GetComponent<ItemPanel>();
            }
            else
            {
                Debug.LogWarning("ToolBarPanel not found in the scene!");
                return; // Skip execution until it is found
            }
        }
        // ttl -= Time.deltaTime; 
        // if(ttl < 0) { Destroy(gameObject); }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return; 
        }
        //Debug.Log($"collected item: {item.name}");
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime);
        if (distance < 0.1f)
        {
            
            if (GameManager.instance.inventoryContainer != null)
            {
                if (item.isLookable==true)
                {
                    SystemMessengerBox.Instance.ShowMessage("right click on the item in the inventory for more information");

                }
                
                GameManager.instance.inventoryContainer.Add(item, count);
                toolBarInventory.Show();
            }
            else
            {
                Debug.LogWarning("no inventory container ");
            }
            Destroy(gameObject);
        }
    }
}


