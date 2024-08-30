using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LibraryLevelManager : MonoBehaviour
{
    public static LibraryLevelManager instance;

    public Animator fadeAnimator;
    public TMP_InputField inputField;
    public Animator inputFieldAnimator;
    public Conversation correctConversation;
    public Conversation incorrectConversation;
    public Conversation entryConversation;
    public string passCode = "58211";

    void Awake()
    {
        if (instance != null)
            Debug.LogError("Instance already existed");
        instance = this;
    }

    void Start()
    {
        StartCoroutine(EntryDialogue());
    }

    IEnumerator EntryDialogue()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.instance.StartConversation(entryConversation);
    }

    public void OnInputFieldEndEdit()
    {
        inputFieldAnimator.SetBool("Prompt", false);

        if (inputField.text == passCode)
        {
            DialogueManager.instance.StartConversation(correctConversation);
            StartCoroutine(LevelCompleted());
        } else {
            DialogueManager.instance.StartConversation(incorrectConversation);
        }
    }

    IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(0.5f);
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Dining Room");
    }

    public void PromptUp()
    {
        inputFieldAnimator.SetBool("Prompt", true);
    }
}
