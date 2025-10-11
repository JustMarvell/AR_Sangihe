using UnityEngine;

public class NPC_Interaction : Interactable
{
    public Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();

        DialogueManager.instance.StartDialogue(dialogue);
    }
}
