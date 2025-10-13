using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask interactionLayer;
    // public LayerMask enemyLayer;     // if using enemy uncomment | for future use
    public float interactionRadius = 3f;
    // public float enemyAttackingRadius = 1f;  // future use if needed
    public static bool onInteraction = false;
    public string interactButtonString = "interactButton";
    public GameObject interactButton;

    public Interactable focus;

    [Space]

    Interactable interactable;

    float[] dist;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactionRadius, interactionLayer) && !onInteraction)
        {
            interactButton?.SetActive(true);
        }
        else
        {
            interactButton?.SetActive(false);
        }
    }

    public void TriggerInteract()
    {
        Debug.Log("Interact Triggered");
        Collider[] coll = Physics.OverlapSphere(transform.position, interactionRadius, interactionLayer);
        dist = new float[coll.Length];

        for (int i = 0; i < coll.Length; i++)
        {
            dist[i] = Vector3.Distance(transform.position, coll[i].transform.position);


            if (i == coll.Length - 1)
            {
                interactable = null;

                float lv = dist.Min();

                for (int l = 0; l < dist.Length; l++)
                {
                    if (dist[l] == lv)
                    {
                        interactable = coll[l].gameObject.GetComponent<Interactable>();

                        if (interactable != null)
                        {
                            SetFocus(interactable);

                            Invoke(nameof(RemoveFocus), .2f);
                        }

                        break;
                    }
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    public void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        interactable = null;
        focus = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
