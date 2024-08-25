using UnityEngine;

public class PlayerInputInteraction : MonoBehaviour
{
    public float interactionRange = 5f;  // Maximum range for interaction
    public LayerMask interactionLayerMask;  // Layer mask for interactable objects
    public LayerMask obstacleLayerMask;  // Layer mask for obstacles (for line of sight)
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
            TryInteract();
        }
    }

    void SearchForInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactionLayerMask);
        GameObject nearestObject = null;
        float minDistance = interactionRange;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, direction, distance, obstacleLayerMask);

                if (lineOfSight.collider == null)
                {
                    nearestObject = hit.gameObject;
                    minDistance = distance;
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
