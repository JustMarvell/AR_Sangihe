using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EducationSceneObjectSpawner : MonoBehaviour
{
    public GameObject currentSpawnedObject;
    public Item currentItemSpawned;

    [Space]

    public Camera aRCamera;
    public ARRaycastManager aRRaycastManager;
    public ARSession aRSession;

    [Space]

    public Vector3 spawnOffset = new(0f, .1f, 0f);
    public bool rotateObject = false;
    public float rotationSpeed = .3f;
    public ObjectSpawnMode objectSpawnMode;


    [Header("Player Prefs Setup")]
    public string Prefs_xSpawnOffset = "ARSpawner_SpawnOffsetX";
    public string Prefs_ySpawnOffset = "ARSpawner_SpawnOffsetY";
    public string Prefs_zSpawnOffset = "ARSpawner_SpawnOffsetZ";
    public string Prefs_rotateObjectToggle = "ARSpawner_ToggleRotateObject";
    public string Prefs_rotationSpeed = "ARSpawner_RotationSpeed";
    public string Prefs_objectSpawnMode = "ARSpawner_ObjectSpawnMode";

    void Start()
    {
        spawnOffset.x = PlayerPrefs.GetFloat(Prefs_xSpawnOffset) == 0 ? 0 : PlayerPrefs.GetFloat(Prefs_xSpawnOffset);
        spawnOffset.y = PlayerPrefs.GetFloat(Prefs_ySpawnOffset) == 0 ? 0 : PlayerPrefs.GetFloat(Prefs_ySpawnOffset);
        spawnOffset.z = PlayerPrefs.GetFloat(Prefs_zSpawnOffset) == 0 ? 0 : PlayerPrefs.GetFloat(Prefs_zSpawnOffset);

        rotateObject = PlayerPrefs.GetInt(Prefs_rotateObjectToggle) == 0 ? false : true;
        rotationSpeed = PlayerPrefs.GetFloat(Prefs_rotationSpeed) == 0 ? 0 : PlayerPrefs.GetFloat(Prefs_rotationSpeed);

        switch (PlayerPrefs.GetInt(Prefs_objectSpawnMode))
        {
            case 0:
                objectSpawnMode = ObjectSpawnMode.LOCAL_CAMERA_HEIGHT;
            break;
            case 1:
                objectSpawnMode = ObjectSpawnMode.CAMERA_CROSSHAIR_POSITION;
            break;
        }
    }

    public void SpawnObject()
    {
        switch (objectSpawnMode)
        {
            case ObjectSpawnMode.LOCAL_CAMERA_HEIGHT:
                SpawnBasedOnLocalCameraHeight();
                break;
            case ObjectSpawnMode.CAMERA_CROSSHAIR_POSITION:
                SpawnAtCrosshair();
                break;
        }
    }

    public void SpawnBasedOnLocalCameraHeight()
    {
        GameObject obj = ObjectPickerUI.instance.currentSelectedItem.itemPrefab;
        
        Vector3 cameraPosition = aRCamera.transform.localPosition;
        cameraPosition.y *= -1f;
        Quaternion cameraRotation = aRCamera.transform.localRotation;
        cameraRotation.x = 0;
        cameraRotation.z = 0;

        if (currentItemSpawned == null || currentItemSpawned != ObjectPickerUI.instance.currentSelectedItem)
        {
            if (currentSpawnedObject != null)
            {
                Destroy(currentSpawnedObject);
            }

            currentItemSpawned = ObjectPickerUI.instance.currentSelectedItem;
            currentSpawnedObject = Instantiate(currentItemSpawned.itemPrefab, cameraPosition + spawnOffset, cameraRotation);
        }
        else
        {
            currentSpawnedObject.transform.SetPositionAndRotation(cameraPosition + spawnOffset, cameraRotation);
        }
    }

    List<ARRaycastHit> aRRaycastHits = new();
    public void SpawnAtCrosshair()
    {
        GameObject obj = ObjectPickerUI.instance.currentSelectedItem.itemPrefab;

        Vector2 screenCenter = new(Screen.width / 2f, Screen.height / 2f);
        if (aRRaycastManager.Raycast(screenCenter, aRRaycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneEstimated))
        {
            Pose hitPose = aRRaycastHits[0].pose;

            if (currentItemSpawned == null || currentItemSpawned != ObjectPickerUI.instance.currentSelectedItem)
            {
                if (currentSpawnedObject != null)
                {
                    Destroy(currentSpawnedObject);
                }
            
                currentItemSpawned = ObjectPickerUI.instance.currentSelectedItem;
                currentSpawnedObject = Instantiate(currentItemSpawned.itemPrefab, hitPose.position + spawnOffset, hitPose.rotation);
            }
            else
            {
                currentSpawnedObject.transform.SetPositionAndRotation(hitPose.position + spawnOffset, hitPose.rotation);
            }
        }
    }

    public void SpawnAtCameraPosition()
    {
        GameObject obj = ObjectPickerUI.instance.currentSelectedItem.itemPrefab;
    }

    public void DestroySpawnedObject()
    {
        if (currentSpawnedObject != null)
        {
            Destroy(currentSpawnedObject);
        }
    }

    void Update()
    {
        if (currentSpawnedObject != null && rotateObject)
        {
            currentSpawnedObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }

    public bool SetRotateToggle
    {
        set
        {
            PlayerPrefs.SetInt(Prefs_rotateObjectToggle, value == false ? 0 : 1);
            rotateObject = value;
        }
    }

    public float SetRotationSpeed
    {
        set
        {
            PlayerPrefs.SetFloat(Prefs_rotationSpeed, value);
            rotationSpeed = value;
        }
    }

    public float SetXOffset
    {
        get
        {
            return spawnOffset.x;
        }
        set
        {
            PlayerPrefs.SetFloat(Prefs_xSpawnOffset, value);
            spawnOffset.x = value;
        }
    }

    public float SetYOffset
    {
        get
        {
            return spawnOffset.y;
        }
        set
        {
            PlayerPrefs.SetFloat(Prefs_ySpawnOffset, value);
            spawnOffset.y = value;
        }
    }

    public float SetZOffset
    {
        get
        {
            return spawnOffset.z;
        }
        set
        {
            PlayerPrefs.SetFloat(Prefs_zSpawnOffset, value);
            spawnOffset.z = value;
        }
    }

    public int SetSpawnMode
    {
        set
        {
            PlayerPrefs.SetInt(Prefs_objectSpawnMode, value);
            switch (value)
            {
                case 0:
                    objectSpawnMode = ObjectSpawnMode.LOCAL_CAMERA_HEIGHT;
                    break;
                case 1:
                    objectSpawnMode = ObjectSpawnMode.CAMERA_CROSSHAIR_POSITION;
                    break;
            }
        }
    }
}

public enum ObjectSpawnMode
{
    LOCAL_CAMERA_HEIGHT,
    CAMERA_CROSSHAIR_POSITION
}
