using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    private Question currentQuestion;
    private Dialogue currentDialogue;

    public Animator animator;

    public static QuestionManager instance;

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple Question Managers detected in scene!");

        instance = this;
    }

    public void StartQuestion(Question question, Dialogue dialogue)
    {
        currentQuestion = question;
        currentDialogue = dialogue;

        // gameObject.SetActive(true); // dont know why this is here
        questionText.text = "";
        animator.SetBool("IsOpen", true);

        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[i];
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
        }
    }

    void OnAnswerSelected(int index)
    {
        if (index == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Correct Answer!");
            // gameObject.SetActive(false);
            animator.SetBool("IsOpen", false);

            DialogueManager.instance.HandleReward(currentDialogue);
            currentDialogue.haveFinishedInteracting = true;

            DialogueManager.instance.StartDialogue(currentDialogue);
        }
        else
        {
            Debug.Log("Wrong Answer, try again!");
            StartQuestion(currentQuestion, currentDialogue);
        }
    }
}
