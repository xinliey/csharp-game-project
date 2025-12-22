using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDragAndDropManager : MonoBehaviour
{
    //once we click the item it will be moving to this script from container editer
    //giving us chance to edit our inventory by clicking 
    public ItemSlot itemSlot;
    [SerializeField] GameObject itemIcon;
    [SerializeField] GameObject QuestItemPanel;
    [SerializeField] TextMeshProUGUI QuestItemData; 
    RectTransform iconTransform;
    Image itemIconImage;
    bool isClicked;
    private void Start()
    {
        QuestItemPanel.SetActive(false);
        itemSlot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    public bool CheckForSale()
    {
        if(itemSlot.item == null) { return false; }
        if(itemSlot.item.canBeSold == false) { return false; }

        return true;
    }
    public void Update()
    {
        if (itemIcon.activeInHierarchy == true)
        {
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {//now check if we click inside or out of inventory panel
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    //Debug.Log("clikcing outside the inventory");

                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0;
                    ItemSpawnManager.instance.SpawnItem(worldPosition, 
                        itemSlot.item, 
                        itemSlot.count);
                    itemSlot.Clear();
                    itemIcon.SetActive(false); 
                    
                }

            }
         

        }
    }
    internal void RightClick(ItemSlot itemSlot)
    {
        if (isClicked == false)
        {
            if (itemSlot.item.isLookable == true)
            {
                QuestItemPanel.SetActive(true);
                string formattedText = itemSlot.item.ItemData.Replace(".", ".\n");
                QuestItemData.text = formattedText;
            }
            isClicked = true;

        }
        else
        {
            QuestItemPanel.SetActive(false);
            isClicked = false;
        }
       
    }
    internal void OnClick(ItemSlot itemSlot)
    {
        //if slot is empty put the item in
        if (this.itemSlot.item == null)
        {
            if (itemSlot.item != null)//ensure that the item we are coping exists
            {
                this.itemSlot.Copy(itemSlot); // Copy the clicked slot's data
                itemSlot.Clear();            // Clear the clicked slot
            }
        }
        else //if there's item , switch place with the current one 
        {
                Item item = itemSlot.item;
                int count = itemSlot.count;
                itemSlot.Copy(this.itemSlot);
                this.itemSlot.Set(item, count);
         }
  
        UpdateIcon();   
    }
    public void UpdateIcon() //the icon on the mouse
    {
        Debug.Log($"UpdateIcon: {itemSlot.item?.name ?? "null"}");
        if (itemSlot.item == null)
        {
            itemIcon.SetActive(false);
        }
        else
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = itemSlot.item.icon;
           // Debug.Log($"Item icon being set: {itemSlot.item.icon}");
        }
    }
}
