using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Dialogue SFX")]
    [field: SerializeField] public EventReference dialogueEnter { get; private set; }
    [field: SerializeField] public EventReference dialogueClose { get; private set; }

    public static FMODEvents instance;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one FMOD Events detected!");
        instance = this;
    }
}