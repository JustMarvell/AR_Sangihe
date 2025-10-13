using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ExplorationSceneManager : MonoBehaviour
{
    public Transform player;
    public Camera _camera;
    public EventDispatcher eventDispatcher;
    public StatePersistence statePersistence;

    public List<GameObject> eventObjects = new();

    public void LoadExplorationScene(string levelID)
    {
        SceneManager.LoadSceneAsync(levelID, LoadSceneMode.Single).completed += _ =>
        {
            InitializePlayerAndCamera();
            SpawnEventObjects();
        };
    }

    void InitializePlayerAndCamera()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = Camera.main;
    }

    void SpawnEventObjects()
    {
        // Placeholder: Load from level data or scene objects
        eventObjects.AddRange(GameObject.FindGameObjectsWithTag("EventObject"));
    }

    void Update()
    {
        DetectEventTrigger();
    }

    void DetectEventTrigger()
    {
        foreach (var eventObj in eventObjects)
        {
            if (Vector3.Distance(player.position, eventObj.transform.position) < 2f)
            {
                EventData data = eventObj.GetComponent<EventDataComponent>().eventData;
                statePersistence.SaveState(player.position, player.rotation, null);
                eventDispatcher.TriggerEvent(data);
                break;
            }
        }
    }

    public void RestoreExplorationState()
    {
        var state = statePersistence.LoadState();
        player.position = state.playerPosition;
        player.rotation = state.playerRotation;
        // camera don't need to adjusted because using cinemachine.
        // let's hope the camera adjust just in time that the perspective is not disturbed by the cut.
    }
}