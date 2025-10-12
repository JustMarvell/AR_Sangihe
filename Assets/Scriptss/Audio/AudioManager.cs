using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("No Audio Manager or Found more than one Audio Manager instance detected!");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
