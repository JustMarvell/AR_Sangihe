using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Vector3 playerPositionScene1 = Vector3.zero;
    public bool hasSavedPosition = false;

    private Dictionary<string, bool> interactableStates = new();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayerPosition(Vector3 position)
    {
        playerPositionScene1 = position;
        hasSavedPosition = true;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPositionScene1;
    }

    public void SetInteractableState(string id, bool state)
    {
        interactableStates[id] = state;
    }

    public bool GetInteractableState(string id)
    {
        return interactableStates.ContainsKey(id) && interactableStates[id];
    }
}
