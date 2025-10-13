using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public ExplorationSceneManager explorationManager;

    void Start()
    {
        Debug.Log("Show Level selection UI");
        LoadLevel("Level1");
    }

    public void LoadLevel(string levelID)
    {
        explorationManager.LoadExplorationScene(levelID);
    }
}