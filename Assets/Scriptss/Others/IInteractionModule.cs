using UnityEngine;

public interface IInteractionModule
{
    void StartInteraction(EventData eventData, GameObject spawnedObject);
    void UpdateInteraction();
    bool EndInteraction(out bool succes, out EventOutcome eventOutcome);
}