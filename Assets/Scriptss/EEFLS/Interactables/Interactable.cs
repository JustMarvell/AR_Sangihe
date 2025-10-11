using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactableRadius = 3f;
    public Transform interactionTransform;
    public bool hasInteracted = false;
    bool isFocused = false;
    Transform player;

    [Header("Debug")]
    public bool showDebug = false;
    public Color debugColor = Color.yellow;

    public virtual void Interact()
    {
        Debug.Log("Interact Start On : " + gameObject.name);

        PlayerInteraction.onInteraction = true;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        if (showDebug)
        {
            Gizmos.color = debugColor;
            Gizmos.DrawWireSphere(interactionTransform.position, interactableRadius);
        }
    }

    void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= interactableRadius)
            {
                hasInteracted = true;
                Interact();
            }
        }
        else if (isFocused && hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance > (interactableRadius + 1f))
            {
                player.GetComponent<PlayerInteraction>().RemoveFocus();
                OnDefocused();
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        hasInteracted = false;
    }
}
