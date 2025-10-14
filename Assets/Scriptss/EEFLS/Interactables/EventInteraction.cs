using UnityEngine;
using UnityEngine.SceneManagement;

public class EventInteraction : Interactable
{
    [SerializeField] private string interactableID;
    public bool finishedInteracted = false;
    public string sceneToLoad;

    void Start()
    {
        if (GameMaster.instance.GetInteractableState(interactableID))
        {
            finishedInteracted = true;
        }
    }

    public override void Interact()
    {
        base.Interact();
        PlayerInteraction.onInteraction = false;

        GameMaster.instance.SetInteractableState(interactableID, true);

        // reload the scene for testing
        // change to load other scene later
        if (sceneToLoad == "")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            GameMaster.instance.LoadSceneAsyncByString(sceneToLoad);
    }
}
