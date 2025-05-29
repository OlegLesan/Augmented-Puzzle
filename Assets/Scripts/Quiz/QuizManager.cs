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

    public QuizUIController quizUIController;

    [Header("SFX Indices")]
    public int correctAnswerSFXIndex = 0;
    public int wrongAnswerSFXIndex = 1;
    public int resultSFXIndex = 2;

    private int currentQuestion = 0;
    private int correctAnswers = 0;
    private bool isAnswering = false;

    private Color[] originalButtonColors;
    private bool colorsInitialized = false;

    void OnEnable()
    {
        RestartQuiz();
    }

    public void RestartQuiz()
    {
        currentQuestion = 0;
        correctAnswers = 0;
        resultPanel.SetActive(false);
        questionPanel.SetActive(true);

        // ✅ Сохраняем оригинальные цвета ОДИН РАЗ — при первом запуске
        if (!colorsInitialized)
        {
            originalButtonColors = new Color[answerButtons.Length];
            for (int i = 0; i < answerButtons.Length; i++)
            {
                originalButtonColors[i] = answerButtons[i].GetComponent<Image>().color;
            }
            colorsInitialized = true;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().color = originalButtonColors[i];
            answerButtons[i].transform.localScale = Vector3.one;
            answerButtons[i].onClick.RemoveAllListeners();
        }

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
        {
            correctAnswers++;
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXByIndex(correctAnswerSFXIndex);
        }
        else
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFXByIndex(wrongAnswerSFXIndex);
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image btnImage = answerButtons[i].GetComponent<Image>();
            Transform btnTransform = answerButtons[i].transform;

            // Цвета из hex
            Color correctColor = new Color32(0x6F, 0xCB, 0x52, 0xFF); // #6FCB52
            Color wrongColor = new Color32(0xCA, 0x52, 0x52, 0xFF);   // #CA5252

            if (i == q.correctAnswerIndex)
                btnImage.color = correctColor;
            else
                btnImage.color = wrongColor;

            if (i == selectedIndex)
                btnTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
            else
                btnTransform.localScale = Vector3.one;
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

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFXByIndex(resultSFXIndex);
    }
}
