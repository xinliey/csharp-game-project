using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gifting : Interactable
{
  public void GiftItem(Character character)
    {
        GiftReceive giftReceive = character.GetComponent<GiftReceive>();
        if(giftReceive == null) { return; }
        

        NPCCharacter npcCharacter = GetComponent<NPCCharacter>();
        giftReceive.GetCurrentNPCData(npcCharacter);
        TalkInteract talkInteract = GetComponent<TalkInteract>();
        giftReceive.GiftingDialogue(talkInteract);
        giftReceive.GiftingProcess(this);
    }
   
}
