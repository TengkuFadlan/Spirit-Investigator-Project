using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedroomLevelManager : MonoBehaviour
{
    public static BedroomLevelManager instance;

    public Animator fadeAnimator;
    public GameObject dollSpirit;
    public Conversation entryDialogue;
    public Item doll1;
    public Item doll2;
    public Item doll3;
    public Item doll4;
    public Item doll5;
    public Item doll6;
    public int dollTaken;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("Instance already existed");
        instance = this;

        dollTaken = 0;
    }

    void Start()
    {
        DialogueManager.instance.OnDialogueEnd += LevelComplete;
        StartCoroutine(EntryDialogue());
    }

    IEnumerator EntryDialogue()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.instance.StartConversation(entryDialogue);
    }

    IEnumerator LevelComplete()
    {
        Destroy(dollSpirit);
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Library");
    }

    public void LevelComplete(string dialogueEventName)
    {
        if (dialogueEventName != "DollCompleted")
            return;

        StartCoroutine(LevelComplete());
    }

    public void GiveDoll()
    {
        if (dollTaken > 5)
            return;

        Item givenDoll = null;
        switch(dollTaken)
        {
            case 0:
               givenDoll = doll1;
               break;
            case 1:
               givenDoll = doll2;
               break;
            case 2:
               givenDoll = doll3;
               break;
            case 3:
               givenDoll = doll4;
               break;
            case 4:
               givenDoll = doll5;
               break;
            case 5:
               givenDoll = doll6;
               break;
        }
        InventoryManager.instance.AddItem(givenDoll);

        dollTaken++;
    }
}
