using UnityEngine;
using FMODUnity;

[System.Serializable]
public class Dialogue
{
    public string name;

    [Space]

    [Tooltip("Leave empty if not used")]
    public GameObject[] obstaclesToDelete;

    [Space]

    [Tooltip("Leave empty if not used")]
    public GameObject[] objectsToActivate;

    [Space]

    public bool haveRequirement = false;
    public Item RequiredItem;
    public Item RewardItem;

    public bool haveFinishedInteracting = false;

    public string interactionID;

    [Header("Sound Settings")]
    public bool useCustomOpenDialogueSound;
    public EventReference openDialogue;
    public bool useCustomCloseDialogueSound;
    public EventReference closeDialogue;

    [Space]

    [TextArea(3, 10)]
    public string[] sentences;
    [TextArea(3, 10)]
    public string[] nextSentences;
    [TextArea(3, 10)]
    public string[] finalSentences;

    [Header("Optional Question")]
    public Question question;
}
