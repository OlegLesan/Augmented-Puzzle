using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuiz", menuName = "Quiz/QuizData")]
public class QuizData : ScriptableObject
{
    public string quizName;
    public List<QuizQuestion> questions;

    [System.Serializable]
    public class ResultOutcome
    {
        [Range(0, 100)]
        public int minPercentage; // ������� ���������, ����� �������� ���� ���������
        [TextArea]
        public string resultText;
    }

    [Header("���������� � ����������� �� ���������")]
    public List<ResultOutcome> results;
}
