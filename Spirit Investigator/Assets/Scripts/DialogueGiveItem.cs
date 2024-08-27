using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGiveItem : MonoBehaviour
{
    public string eventName;
    public Item item;

    void OnDialogueEndFunction(string firedEventName)
    {
        if (firedEventName != eventName)
            return;

        InventoryManager.instance.AddItem(item);
    }

    void Start()
    {
        DialogueManager.instance.OnDialogueEnd -= OnDialogueEndFunction;
        DialogueManager.instance.OnDialogueEnd += OnDialogueEndFunction;
    }

    void OnEnable()
    {
        if (DialogueManager.instance == null)
            return;

        DialogueManager.instance.OnDialogueEnd -= OnDialogueEndFunction;
        DialogueManager.instance.OnDialogueEnd += OnDialogueEndFunction;
    }

    void OnDisable()
    {
        DialogueManager.instance.OnDialogueEnd -= OnDialogueEndFunction;
    }
}
