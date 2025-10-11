using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    Button button;
    public string sceneToLoad;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => OnButtonClicked());
    }

    void OnButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
