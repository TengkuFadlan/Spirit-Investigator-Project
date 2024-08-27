using UnityEngine;

public class DialologueInteraction : MonoBehaviour, IInteractable
{

    public Conversation conversation;

    public void Interact()
    {
        DialogueManager.instance.StartConversation(conversation);
    }
}
