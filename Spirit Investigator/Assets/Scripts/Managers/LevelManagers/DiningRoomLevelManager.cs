using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiningRoomLevelManager : MonoBehaviour
{
    public Animator fadeAnimator;
    public GameObject[] tableGameObjects;
    public GameObject[] targetGameObjects;
    public Conversation entryDialogue;
    public Conversation completedConversation;
    public Conversation toBeContinuedConversation;
    public float proximityThreshold = 2.0f;
    public bool solved = false;

    void Start()
    {
        StartCoroutine(EntryDialogue());
    }

    IEnumerator EntryDialogue()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.instance.StartConversation(entryDialogue);
    }

    IEnumerator Solved()
    {
        DialogueManager.instance.StartConversation(completedConversation);
        yield return new WaitForSeconds(0.1f);
        fadeAnimator.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        DialogueManager.instance.StartConversation(toBeContinuedConversation);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        bool allTableObjectsNearTargets = true;

        foreach (GameObject tableObject in tableGameObjects)
        {
            bool isNearAnyTarget = false;

            foreach (GameObject targetObject in targetGameObjects)
            {
                float distance = Vector2.Distance(tableObject.transform.position, targetObject.transform.position);
                if (distance <= proximityThreshold)
                {
                    isNearAnyTarget = true;
                    break;
                }
            }

            if (!isNearAnyTarget)
            {
                allTableObjectsNearTargets = false;
                break;
            }
        }

        if (allTableObjectsNearTargets)
        {
            if (solved)
                return;

            solved = true;
            StartCoroutine(Solved());
        }
    }
}
