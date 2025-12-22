using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{
    //sometime price that store buy from you and you sell to store are different 
    public float buyFromPlayerMultip = 0.7f;
    public float sellToPlayerMultip = 1f;
    public ItemContainer storeContent; //showing what item is available in the store
    [SerializeField] List<Item> itemrestock =new List<Item>();
    public override void Interact(Character character)
    {
        Trading trading = character.GetComponent<Trading>();
        if(trading == null) { return; }
        trading.BeginTrading(this);
    }

    public void Restocking()
    {
        //Debug.Log("restocking item to the store");
        int randomIndex = Random.Range(0, itemrestock.Count);
        Item afterRandom = itemrestock[randomIndex];
         storeContent.Add(afterRandom,1);
    }
}
