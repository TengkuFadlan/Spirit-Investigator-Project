using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPieceInteraction : MonoBehaviour, IInteractable
{
    public bool taken = false;
    public Conversation conversation;

    public void Interact()
    {
        if (taken)
            return;

        taken = true;
        
        BedroomLevelManager.instance.GiveDoll();
        DialogueManager.instance.StartConversation(conversation);
    }
}
