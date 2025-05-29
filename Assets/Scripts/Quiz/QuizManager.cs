using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public QuizData quizData;

    public TMP_Text questionText;
    public Button[] answerButtons;
    public TMP_Text resultText;

    public GameObject questionPanel;
    public GameObject resultPanel;

    private int currentQuestion = 0;
    private int correctAnswers = 0;
    private bool isAnswering = false;

    private Color[] originalButtonColors; // сохраняем цвета из инспектора

    void Start()
    {
        // Сохраняем начальные цвета кнопок
        originalButtonColors = new Color[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            originalButtonColors[i] = answerButtons[i].GetComponent<Image>().color;
        }

        resultPanel.SetActive(false);
        questionPanel.SetActive(true);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        isAnswering = true;

        if (currentQuestion >= quizData.questions.Count)
        {
            ShowResult();
            return;
        }

        var q = quizData.questions[currentQuestion];
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TMP_Text txt = answerButtons[i].GetComponentInChildren<TMP_Text>();
            txt.text = q.answers[i];

            // Сброс цвета и масштаба
            answerButtons[i].GetComponent<Image>().color = originalButtonColors[i];
            answerButtons[i].transform.localScale = Vector3.one;

            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (!isAnswering) return;
        isAnswering = false;

        var q = quizData.questions[currentQuestion];
        bool isCorrect = selectedIndex == q.correctAnswerIndex;

        if (isCorrect)
            correctAnswers++;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image btnImage = answerButtons[i].GetComponent<Image>();
            Transform btnTransform = answerButtons[i].transform;

            // Подсветка правильной/неправильной
            if (i == q.correctAnswerIndex)
            {
                btnImage.color = Color.green;
            }
            else
            {
                btnImage.color = Color.red;
            }

            // Увеличиваем выбранную кнопку
            if (i == selectedIndex)
            {
                btnTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
            }
            else
            {
                btnTransform.localScale = Vector3.one;
            }
        }

        StartCoroutine(NextQuestionAfterDelay(1.5f));
    }

    IEnumerator NextQuestionAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentQuestion++;
        ShowQuestion();
    }

    void ShowResult()
    {
        questionPanel.SetActive(false);
        resultPanel.SetActive(true);

        string resultTextOutput = "Нет подходящего результата";

        QuizData.ResultOutcome bestFit = null;
        foreach (var outcome in quizData.results)
        {
            if (correctAnswers >= outcome.minCorrectAnswers)
            {
                if (bestFit == null || outcome.minCorrectAnswers > bestFit.minCorrectAnswers)
                    bestFit = outcome;
            }
        }

        if (bestFit != null)
            resultTextOutput = bestFit.resultText;

        resultText.text = $"Răspunsuri corecte: {correctAnswers}/{quizData.questions.Count}\n\n{resultTextOutput}";
    }
}
