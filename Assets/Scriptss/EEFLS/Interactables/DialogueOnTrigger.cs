using UnityEngine;

public class DialogueOnTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogueManager.instance.StartDialogue(dialogue);
            Destroy(gameObject);
        }
    }
}