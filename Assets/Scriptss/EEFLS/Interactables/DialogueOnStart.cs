using UnityEngine;

public class DialogueOnStart : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        DialogueManager.instance.StartDialogue(dialogue);

        Destroy(gameObject);
    }
}
