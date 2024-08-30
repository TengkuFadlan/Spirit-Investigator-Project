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
    public AudioSource typeAudioSource;
    public float typingSpeed = 0.001f;

    Queue<Dialogue> conversationList = new();
    string dialogueEventName;
    Coroutine typingCoroutine;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("Instance already existed");
        instance = this;
    }

    void ReadDialogue(Dialogue dialogue)
    {
        typeAudioSource.Play();

        dialogBoxAnimator.SetBool("Open", true);

        speakerText.text = dialogue.speakerText;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence(dialogue.sentenceText));

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

    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null;
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

        Time.timeScale = 0;

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
        typeAudioSource.Stop();
        conversationList.Clear();
        dialogBoxAnimator.SetBool("Open", false);
        leftSpeakerAnimator.SetBool("Speaking", false);
        leftSpeakerAnimator.SetBool("Listening", false);
        rightSpeakerAnimator.SetBool("Speaking", false);
        rightSpeakerAnimator.SetBool("Listening", false);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        Time.timeScale = 1;
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
