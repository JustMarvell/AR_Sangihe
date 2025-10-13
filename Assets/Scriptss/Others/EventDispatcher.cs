using UnityEngine;

public class EventDispatcher : MonoBehaviour
{
    public ARSceneManager arManager;
    public ExplorationSceneManager explorationManager;

    public void TriggerEvent(EventData eventData)
    {
        // pause exploration input
        explorationManager.enabled = false;
        // TODO : Show Pre-AR UI
        Debug.Log("Entering AR Mode....");
        arManager.LoadARSceneAsync(eventData);
    }

    public void OnARComplete(bool succes, EventOutcome outcome)
    {
        // apply outcomes | inventory, etc
        if (outcome.completed)
        {
            // foreach (var item in outcome.inventoryChanges)
            // {
            //     Debug.Log($"Added to inventory: {item}");
            // }
            Debug.Log("AR Competed | outome succes");
        }
        explorationManager.enabled = true;
        // explorationManager.RestoreExplorationState();
    }
}