using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class ARSceneManager : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;
    private EventData currentEvent;
    private IInteractionModule currentModule;
    private GameObject spawnedObject;

    public string aRSceneToLoad;

    public async void LoadARSceneAsync(EventData eventData)
    {
        currentEvent = eventData;
        currentModule = eventData.interactionModule as IInteractionModule;
        await SceneManager.LoadSceneAsync(aRSceneToLoad, LoadSceneMode.Additive).AsTask();
        StartARSession();
    }

    void StartARSession()
    {
        planeManager = FindFirstObjectByType<ARPlaneManager>();
        raycastManager = FindFirstObjectByType<ARRaycastManager>();
        planeManager.enabled = true;
        StartCoroutine(DetectSpawnPoint());
    }

    IEnumerator DetectSpawnPoint()
    {
        while (spawnedObject == null)
        {
            var hits = new List<ARRaycastHit>();
            if (Input.touchCount > 0 && raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                Pose pose = hits[0].pose;
                if (ValidateSpawnPosition(pose.position))
                {
                    spawnedObject = Instantiate(currentEvent.prefabToSpawn, pose.position, pose.rotation);
                    currentModule.StartInteraction(currentEvent, spawnedObject);
                }
            }
            yield return null;
        }
    }

    bool ValidateSpawnPosition(Vector3 posePosition)
    {
        // Example: Check if position is on a horizontal plane
        return true; // Add specific validation based on event parameters

    }

    void Update()
    {
        if (currentModule != null)
        {
            currentModule.UpdateInteraction();
        }
    }

    public void EndARSession(bool succes, EventOutcome outcome)
    {
        planeManager.enabled = false;
        Destroy(spawnedObject);
        SceneManager.UnloadSceneAsync(aRSceneToLoad);
        FindAnyObjectByType<EventDispatcher>().OnARComplete(succes, outcome);
    }
}