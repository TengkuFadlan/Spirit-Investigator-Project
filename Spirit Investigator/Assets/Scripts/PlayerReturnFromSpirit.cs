using UnityEngine;

public class PlayerReturnFromSpirit : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        SpiritManager.instance.ExitSpirit();
    }
}
