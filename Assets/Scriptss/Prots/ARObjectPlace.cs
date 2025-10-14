using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectPlace : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Camera arCamera;

    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    
    public Vector3 YOFFSET = new(0, .002f, 0);

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    public void SpawnAtCrosshair()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(prefabToSpawn, hitPose.position + YOFFSET, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hitPose.position + YOFFSET, hitPose.rotation);
            }
        }
    }

    public void SpawnBasedOnHeight()
    {
        Vector3 cameraPosition = Camera.main.transform.localPosition;
        cameraPosition.y *= -1;
        Quaternion cameraRotation = Camera.main.transform.localRotation;
        cameraRotation.x = 0;
        cameraRotation.z = 0;

        if (spawnedObject == null)
            spawnedObject = Instantiate(prefabToSpawn, cameraPosition, cameraRotation);
        else
            spawnedObject.transform.SetPositionAndRotation(cameraPosition, cameraRotation);
    }
}
