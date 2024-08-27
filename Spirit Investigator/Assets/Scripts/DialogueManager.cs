using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Action<string> OnDialogueEnd;
    public bool isDialogueActive = false;

    public Animator dialogBoxAnimator;
    public Animator leftSpeakerAnimator;
    public Animator rightSpeakerAnimator;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI sentenceText;
    public RawImage leftSpeakerImage;
    public RawImage rightSpeakerImage;

    Queue<Dialogue> conversationList = new();
    string dialogueEventName;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void ReadDialogue(Dialogue dialogue)
    {
        dialogBoxAnimator.SetBool("Open", true);

        speakerText.text = dialogue.speakerText;
        sentenceText.text = dialogue.sentenceText;

        if (dialogue.leftCharacterImage != null)
        {
            leftSpeakerImage.texture = dialogue.leftCharacterImage;
            leftSpeakerAnimator.SetBool("Speaking", dialogue.isLeftSpeaking);
            leftSpeakerAnimator.SetBool("Listening", !dialogue.isLeftSpeaking);
        }
        else
        {
            leftSpeakerAnimator.SetBool("Speaking", false);
            leftSpeakerAnimator.SetBool("Listening", false);
        }

        if (dialogue.rightCharacterImage != null)
        {
            rightSpeakerImage.texture = dialogue.rightCharacterImage;
            rightSpeakerAnimator.SetBool("Speaking", dialogue.isRightSpeaking);
            rightSpeakerAnimator.SetBool("Listening", !dialogue.isRightSpeaking);
        }
        else
        {
            rightSpeakerAnimator.SetBool("Speaking", false);
            rightSpeakerAnimator.SetBool("Listening", false);
        }
    }

    public void StartConversation(Conversation conversation)
    {
        CancelConversation();

        InventoryManager.instance.SetInventoryUI(false);

        foreach (Dialogue dialogue in conversation.dialogues)
        {
            conversationList.Enqueue(dialogue);
        }

        dialogueEventName = conversation.eventName;
        isDialogueActive = true;

        ReadDialogue(conversationList.Dequeue());
    }

    public void EndConversation()
    {
        CancelConversation();

        OnDialogueEnd?.Invoke(dialogueEventName);
    }

    public void CancelConversation()
    {
        isDialogueActive = false;
        conversationList.Clear();
        dialogBoxAnimator.SetBool("Open", false);
        leftSpeakerAnimator.SetBool("Speaking", false);
        leftSpeakerAnimator.SetBool("Listening", false);
        rightSpeakerAnimator.SetBool("Speaking", false);
        rightSpeakerAnimator.SetBool("Listening", false);
    }

    void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (conversationList.Count > 0)
                {
                    ReadDialogue(conversationList.Dequeue());
                }
                else
                {
                    EndConversation();
                }
            }

        }
    }
}
