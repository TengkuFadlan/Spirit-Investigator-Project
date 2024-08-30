using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputInteraction : MonoBehaviour
{
    public float interactionRange = 5f;
    private GameObject currentInteractableObject;
    private Highlightable currentHighlight;

    void FixedUpdate()
    {
        SearchForInteractable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.timeScale != 0)
                TryInteract();
        }
    }

    void SearchForInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        GameObject nearestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    nearestObject = hit.gameObject;
                    closestDistance = distance;
                }
            }
        }

        if (nearestObject != currentInteractableObject)
        {
            if (currentHighlight != null)
            {
                currentHighlight.Highlight(false);
                currentHighlight = null;
            }

            currentInteractableObject = nearestObject;
            if (currentInteractableObject != null)
            {
                currentHighlight = currentInteractableObject.GetComponent<Highlightable>();
                if (currentHighlight != null)
                {
                    currentHighlight.Highlight(true);
                }
            }
        }
    }

    void TryInteract()
    {
        if (currentInteractableObject != null)
        {
            IInteractable interaction = currentInteractableObject.GetComponent<IInteractable>();
            if (interaction != null)
            {
                interaction.Interact();
            }
        }
    }

    void OnDisable()
    {
        if (currentHighlight != null)
        {
            currentHighlight.Highlight(false);
        }
        currentInteractableObject = null;
        currentHighlight = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
