using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionUnityEvent : MonoBehaviour, IInteractable
{
    public UnityEvent unityEvent;

    public void Interact()
    {
        unityEvent.Invoke();
    }
}
