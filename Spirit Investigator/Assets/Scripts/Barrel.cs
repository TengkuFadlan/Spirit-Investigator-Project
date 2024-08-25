using UnityEngine;

public class Barrel : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("I'm a barrel");
    }
}
