using UnityEngine;

[System.Serializable]
public class Question
{
    public bool haveQuestion = false;

    [TextArea(3, 10)]
    public string questionText;

    [TextArea(3, 5)]
    public string[] answers;

    public int correctAnswerIndex;
}
