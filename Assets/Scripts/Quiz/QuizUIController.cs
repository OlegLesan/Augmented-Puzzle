using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizUIController : MonoBehaviour
{
    [Header("Canvas-�")]
    public GameObject canvasSelection;
    public GameObject canvasQuestions;

    [Header("������� � �������")]
    public GameObject[] quizObjects;

    private GameObject currentActiveQuiz;

    public void StartQuiz(int index)
    {
        if (index < 0 || index >= quizObjects.Length)
        {
            Debug.LogError("�������� ������ �����: " + index);
            return;
        }

        foreach (var quiz in quizObjects)
        {
            quiz.SetActive(false);
        }

        currentActiveQuiz = quizObjects[index];
        currentActiveQuiz.SetActive(true);

        canvasSelection.SetActive(false);
        canvasQuestions.SetActive(true);

        QuizManager qm = currentActiveQuiz.GetComponent<QuizManager>();
        if (qm == null) qm = currentActiveQuiz.GetComponentInChildren<QuizManager>();
        if (qm != null) qm.RestartQuiz();
    }

    public void ReturnToSelection()
    {
        if (currentActiveQuiz != null)
        {
            currentActiveQuiz.SetActive(false);
            currentActiveQuiz = null;
        }

        canvasQuestions.SetActive(false);
        canvasSelection.SetActive(true);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
