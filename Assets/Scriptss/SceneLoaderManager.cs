using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public GameObject aRSceneLoadingScreen;
    public GameObject mainMenuSceneLoadingScreen;
    public GameObject explorationSceneLoadingScreen;
    public GameObject educationSceneLoadingScreen;

    [Space]

    public string aRSceneName;
    public string mainMenuSceneName;
    public string explorationSceneName;
    public string educationSceneName;

    [Space]

    [TextArea(3, 5)] public string[] aRSceneLoadingHintText;
    [TextArea(3, 5)] public string[] mainMenuLoadingHintText;
    [TextArea(3, 5)] public string[] explorationSceneLoadingHintText;
    [TextArea(3, 5)] public string[] educationSceneLoadingHintText;

    [Space]

    public float afterLoadingDelay = 1f;
    public Slider progressBar;
    public TextMeshProUGUI loadingHintText;
    private float totalSceneProgress = 0;

    public List<AsyncOperation> scenesLoading = new();

    void Start()
    {
        SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive);

        SceneManager.sceneLoaded += SceneLoadingComplete;
        SceneManager.sceneUnloaded += SceneUnloadComplete;
    }

    private void SceneUnloadComplete(Scene scene)
    {
        
    }

    private void SceneLoadingComplete(Scene scene, LoadSceneMode loadSceneMode)
    {
        ARSession aRSession = FindFirstObjectByType<ARSession>();
        if (aRSession != null)
            StartCoroutine(ARSessionManagerHelper.DelayedReset(aRSession));
    }

    public void SwitchToARScene()
    {
        progressBar = null;
        loadingHintText = null;

        aRSceneLoadingScreen.SetActive(true);

        progressBar = aRSceneLoadingScreen.transform.GetChild(1).GetComponent<Slider>();
        progressBar.value = 0;

        loadingHintText = aRSceneLoadingScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        loadingHintText.text = GetRandomHintText(aRSceneLoadingHintText);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        scenesLoading.Add(SceneManager.LoadSceneAsync(aRSceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadingProgress(aRSceneLoadingScreen));
    }

    public void SwitchToEducationScene()
    {
        progressBar = null;
        loadingHintText = null;

        educationSceneLoadingScreen.SetActive(true);
        
        progressBar = educationSceneLoadingScreen.transform.GetChild(1).GetComponent<Slider>();
        progressBar.value = 0;

        loadingHintText = educationSceneLoadingScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        loadingHintText.text = GetRandomHintText(educationSceneLoadingHintText);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        scenesLoading.Add(SceneManager.LoadSceneAsync(educationSceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadingProgress(educationSceneLoadingScreen));
    }

    public void SwitchToMainMenuScene()
    {   
        progressBar = null;
        loadingHintText = null;
        
        mainMenuSceneLoadingScreen.SetActive(true);

        progressBar = mainMenuSceneLoadingScreen.transform.GetChild(1).GetComponent<Slider>();
        progressBar.value = 0;

        loadingHintText = mainMenuSceneLoadingScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        loadingHintText.text = GetRandomHintText(mainMenuLoadingHintText);

        if (SceneManager.loadedSceneCount > 2)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
            scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(2)));
        }
        else
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        }

        scenesLoading.Add(SceneManager.LoadSceneAsync(mainMenuSceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadingProgress(mainMenuSceneLoadingScreen));
    }

    public void SwitchToExplorationScene()
    {
        progressBar = null;
        loadingHintText = null;
        
        explorationSceneLoadingScreen.SetActive(true);

        progressBar = explorationSceneLoadingScreen.transform.GetChild(1).GetComponent<Slider>();
        progressBar.value = 0;

        loadingHintText = explorationSceneLoadingScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        loadingHintText.text = GetRandomHintText(explorationSceneLoadingHintText);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        scenesLoading.Add(SceneManager.LoadSceneAsync(explorationSceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadingProgress(explorationSceneLoadingScreen));
    }
    
    public IEnumerator GetSceneLoadingProgress(GameObject loadingScreen)
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                scenesLoading[i].allowSceneActivation = false;
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = totalSceneProgress / scenesLoading.Count * 100f;
                progressBar.value = Mathf.RoundToInt(totalSceneProgress);

                if (totalSceneProgress >= 80)
                    scenesLoading[i].allowSceneActivation = true;

                yield return null;
            }
        }

        yield return new WaitForSeconds(afterLoadingDelay);
        loadingScreen.SetActive(false);
    }

    private string GetRandomHintText(string[] hintText)
    {
        string randomText = hintText[Random.Range(0, hintText.Length)];

        return randomText;
    }

    public string GetRandomHintStringFromAllList()
    {
        int randomList = Random.Range(0, 3);
        string randomText = "";
        switch (randomList)
        {
            case 0:
                randomText = aRSceneLoadingHintText[Random.Range(0, aRSceneLoadingHintText.Length)];
            break;
            case 1:
                randomText = mainMenuLoadingHintText[Random.Range(0, mainMenuLoadingHintText.Length)];
            break;
            case 2:
                randomText = explorationSceneLoadingHintText[Random.Range(0, explorationSceneLoadingHintText.Length)];
            break;
            case 3:
                randomText = educationSceneLoadingHintText[Random.Range(0, educationSceneLoadingHintText.Length)];
            break;
        }

        return randomText;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }
}

public class SceneListManager
{
    
}
