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
        public int minPercentage; // Минимум процентов, чтобы получить этот результат
        [TextArea]
        public string resultText;
    }

    [Header("Результаты в зависимости от процентов")]
    public List<ResultOutcome> results;
}
