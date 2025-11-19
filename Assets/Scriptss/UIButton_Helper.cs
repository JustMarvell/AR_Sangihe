using UnityEngine;

public class UIButton_Helper : MonoBehaviour
{
    public void LoadARScene()
    {
        SceneLoaderManager.instance.SwitchToARScene();
    }

    public void LoadExplorationScene()
    {
        SceneLoaderManager.instance.SwitchToExplorationScene();
    }

    public void LoadMainMenuScene()
    {
        SceneLoaderManager.instance.SwitchToMainMenuScene();
    }

    public void LoadEducationScene()
    {
        SceneLoaderManager.instance.SwitchToEducationScene();
    }

    public void QuitGame()
    {
        SceneLoaderManager.instance.QuitGame();
    }
}