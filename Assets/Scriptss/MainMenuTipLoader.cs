using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuTipLoader : MonoBehaviour
{
    public TextMeshProUGUI tipText;

    public void OnEnable()
    {
        if (tipText != null && SceneLoaderManager.instance != null)
        {
            tipText.text = SceneLoaderManager.instance.GetRandomHintStringFromAllList();
        }
    }
}
