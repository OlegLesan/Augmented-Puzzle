using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public QuizData quizData;

    public TMP_Text questionText;
    public Button[] answerButtons;
    public TMP_Text resultText;

    public GameObject questionPanel; // панель с вопросом и кнопками
    public GameObject resultPanel;   // панель с текстом результата

    private int currentQuestion = 0;
    private int totalScore = 0;

    void Start()
    {
        resultPanel.SetActive(false); // скрываем результат
        questionPanel.SetActive(true); // показываем панель с вопросами
        ShowQuestion();
    }

    void ShowQuestion()
    {
        if (currentQuestion >= quizData.questions.Count)
        {
            ShowResult();
            return;
        }

        var q = quizData.questions[currentQuestion];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = q.answers[i];

            int score = q.answerScores[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswer(score));
        }
    }

    void OnAnswer(int score)
    {
        totalScore += score;
        currentQuestion++;
        ShowQuestion();
    }

    void ShowResult()
    {
        questionPanel.SetActive(false); // скрываем панель с вопросами
        resultPanel.SetActive(true);    // показываем результат

        float maxScore = quizData.questions.Count * 30f; // если максимум за ответ — 30
        float percentage = (totalScore / maxScore) * 100f;

        string resultTextOutput = "Нет подходящего результата";

        // Найти лучший результат
        QuizData.ResultOutcome bestFit = null;
        foreach (var outcome in quizData.results)
        {
            if (percentage >= outcome.minPercentage)
            {
                if (bestFit == null || outcome.minPercentage > bestFit.minPercentage)
                    bestFit = outcome;
            }
        }

        if (bestFit != null)
            resultTextOutput = bestFit.resultText;

        resultText.text = resultTextOutput;
    }
}
