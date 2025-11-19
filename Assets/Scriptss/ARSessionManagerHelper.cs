using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

public class ARSessionManagerHelper : MonoBehaviour
{
    public ARSession aRSession;
    // public ARAnchorManager aRAnchorManager;

    void Start()
    {
        // if (aRSession != null)
        //     aRSession.Reset();
    }

    public void DisableARSession()
    {
        if (aRSession != null)
        {
            aRSession.Reset();
            aRSession.enabled = false;
            if (aRSession.subsystem != null)
                aRSession.subsystem.Stop();
        }
        XRGeneralSettings.Instance.Manager.StopSubsystems();

        var xrManagerSettings = UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager;
        xrManagerSettings.DeinitializeLoader();
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); // reload current scene
        xrManagerSettings.InitializeLoaderSync();

        SceneLoaderManager.instance.SwitchToMainMenuScene();
    }

    public static IEnumerator DelayedReset(ARSession _aRSession)
    {
        yield return new WaitUntil(() => _aRSession.subsystem.running); // Wait for active session
        _aRSession.Reset();
        
        // LoaderUtility.Initialize();

        // if (_aRSession != null)
        // {
        //     _aRSession.Reset();
        //     _aRSession.enabled = false;
        //     if (_aRSession.subsystem != null)
        //         _aRSession.subsystem.Stop();
        // }
        // XRGeneralSettings.Instance.Manager.StopSubsystems();
        // XRGeneralSettings.Instance.Manager.activeLoader.Deinitialize();

        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            // Khởi động các subsystem, bao gồm cả AR Camera
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            // Kiểm tra nếu Camera Subsystem có sẵn
            if (XRGeneralSettings.Instance.Manager.activeLoader != null)
            {
                _aRSession.enabled = true;
                Debug.Log("AR Camera Subsystem started successfully.");
            }
            else
            {
                Debug.LogError("No active XRCameraSubsystem is available.");
            }
        }
        else
        {
            Debug.LogError("Failed to initialize XR Loader.");
        }
    }
}
