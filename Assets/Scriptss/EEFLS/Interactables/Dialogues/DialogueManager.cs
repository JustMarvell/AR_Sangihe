using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText, dialogueText;

    public Animator animator;
    public static bool IsInDialogue;
    public bool isInSecondDialogue = false;
    public float dialogeTextSpeed = .1f;

    public GameObject[] deactivateWhenInDialogue;

    // TODO : Add Game Master script
    // GameMaster gm;

    Queue<string> sentences;
    Queue<EventInstance> voiceOvers;
    EventInstance currentVoiceOver;
    Dialogue dlg;

    public static DialogueManager instance;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("WARNING! : More than one Dialogue Manager Detected in scene!");

        instance = this;
    }

    void Start()
    {
        sentences = new();
        voiceOvers = new();
        // gm = GameMaster.instance;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // play sound
        if (dialogue.useCustomOpenDialogueSound)
            AudioManager.instance.PlayOneShot(dialogue.openDialogueSound, Camera.main.transform.position);
        else
            AudioManager.instance.PlayOneShot(FMODEvents.instance.dialogueEnter, Camera.main.transform.position);

        Debug.Log("Dialog Start with : " + dialogue.name);
        PlayerInteraction.onInteraction = true;
        IsInDialogue = true;
        dlg = dialogue;
        // gm.isPaused = true; // wip

        foreach (GameObject gameObject in deactivateWhenInDialogue)
        {
            gameObject.SetActive(false);
        }

        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        // NEXT FEATURE MAYBE???
        // Waypoint navigation system??
        // Chained Quest system??

        sentences.Clear();
        voiceOvers.Clear();

        if (!isInSecondDialogue)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            foreach (EventReference voices in dialogue.sentencesVO)
            {
                voiceOvers.Enqueue(AudioManager.instance.CreateEventInstance(voices));
            }
        }
        else
        {
            foreach (string sentence in dialogue.nextSentences)
            {
                sentences.Enqueue(sentence);
            }

            foreach (EventReference voices in dialogue.nextSentencesVO)
            {
                voiceOvers.Enqueue(AudioManager.instance.CreateEventInstance(voices));
            }
        }

        if (GameMaster.instance.GetInteractableState(dlg.interactionID))
        {
            sentences.Clear();
            foreach (string sentence in dialogue.finalSentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        // TODO : Add voice system

        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if (!IsInDialogue)
            return;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (dlg.useVoiceOver)
            currentVoiceOver.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        string sentence = sentences.Dequeue();

        if (dlg.useVoiceOver)
        {
            EventInstance _voiceOver = voiceOvers.Dequeue();
            currentVoiceOver = _voiceOver;
        }

        StopAllCoroutines();
        if (dlg.useVoiceOver)
            currentVoiceOver.start();

        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        // play dialogue close sound
        if (dlg.useCustomCloseDialogueSound)
            AudioManager.instance.PlayOneShot(dlg.closeDialogueSound, Camera.main.transform.position);
        else
            AudioManager.instance.PlayOneShot(FMODEvents.instance.dialogueClose, Camera.main.transform.position);

        if (dlg.useVoiceOver)
        {
            currentVoiceOver.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentVoiceOver.release();
        }

        PlayerInteraction.onInteraction = false;
        Debug.Log("End of dialogue");
        animator.SetBool("IsOpen", false);
        IsInDialogue = false;
        // gm.isPaused = false;

        if (dlg.objectsToActivate.Length > 0)
        {
            foreach (GameObject obj in dlg.objectsToActivate)
            {
                obj.SetActive(true);
            }
        }

        if (dlg.obstaclesToDelete.Length > 0)
        {
            foreach (GameObject obs in dlg.obstaclesToDelete)
            {
                DestroyImmediate(obs);
            }
        }

        if (dlg.haveRequirement && !isInSecondDialogue)
        {
            bool requirementFulfiled = Inventory.instance.CheckItem(dlg.RequiredItem);
            if (requirementFulfiled)
            {
                Debug.Log("Requirement fulfilled, continue to next sentence");
                Inventory.instance.Remove(Inventory.instance.GetItem(dlg.RequiredItem));

                isInSecondDialogue = true;
                StartDialogue(dlg);
                return;
            }
            else
            {
                Debug.Log("Missing the required item, please find it first!");
                isInSecondDialogue = false;

                FinishedInteractionCleanup();
                return;
            }
        }

        if (dlg.question.haveQuestion && !dlg.haveFinishedInteracting)
        {
            QuestionManager.instance.StartQuestion(dlg.question, dlg);
            return;
        }

        HandleReward(dlg);

        foreach (GameObject gameObject in deactivateWhenInDialogue)
        {
            gameObject.SetActive(true);
        }

        isInSecondDialogue = false;

        PlayerInteraction.onInteraction = false;
        dlg = null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / dialogeTextSpeed * Time.deltaTime);
        }
    }

    public void HandleReward(Dialogue dialogue)
    {
        if (!dialogue.haveFinishedInteracting)
        {
            if (dialogue.RewardItem != null && !dialogue.haveRequirement)
            {
                bool wasReceived = Inventory.instance.Add(dialogue.RewardItem);
                if (wasReceived) Debug.Log("Received item : " + dialogue.RewardItem.name);
                else Debug.Log("Failed to receive item : " + dialogue.RewardItem.name);

                dialogue.haveFinishedInteracting = true;
                GameMaster.instance.SetInteractableState(dialogue.interactionID, true);
            }

            if (isInSecondDialogue && dialogue.RewardItem != null)
            {
                bool wasReceived = Inventory.instance.Add(dialogue.RewardItem);
                if (wasReceived) Debug.Log("Received item : " + dialogue.RewardItem.name);
                else Debug.Log("Failed to receive item : " + dialogue.RewardItem.name);

                dialogue.haveFinishedInteracting = true;
                GameMaster.instance.SetInteractableState(dialogue.interactionID, true);
            }
        }
    }

    public void FinishedInteractionCleanup()
    {
        foreach (GameObject go in deactivateWhenInDialogue)
        {
            go.SetActive(true);
        }

        isInSecondDialogue = false;
        PlayerInteraction.onInteraction = false;
        dlg = null;
    }
}
