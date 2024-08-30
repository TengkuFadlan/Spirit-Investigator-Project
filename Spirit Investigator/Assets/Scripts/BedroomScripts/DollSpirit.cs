using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollSpirit : MonoBehaviour, IInteractable
{
    public bool taken = false;
    public Conversation getDollConversation;
    public Conversation doneConversation;

    public void Interact()
    {
        if (BedroomLevelManager.instance.dollTaken == 6)
        {
            DialogueManager.instance.StartConversation(doneConversation);
        }
        else
        {
            DialogueManager.instance.StartConversation(getDollConversation);
        }
    }
}
