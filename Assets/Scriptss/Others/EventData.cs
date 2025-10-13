using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Event Data", menuName ="AR/Event Data")]
public class EventData : ScriptableObject
{
    public string eventType;
    public GameObject prefabToSpawn;
    public MonoBehaviour interactionModule;
    public List<IInteractionModule> parameters;
    public string eventID;
}