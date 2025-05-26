using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quizButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton; // ← Новая кнопка Back

    [Header("Scene Settings")]
    [SerializeField] private string startSceneName;
    [SerializeField] private string quizSceneName;

    [Header("UI Panels")]
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject optionsPanel;

    private void Start()
    {
        startButton.onClick.AddListener(HandleStartGame);
        quizButton.onClick.AddListener(HandleOpenQuiz);
        optionsButton.onClick.AddListener(HandleOpenOptions);
        exitButton.onClick.AddListener(HandleExitGame);
        backButton.onClick.AddListener(HandleBackToMenu); // ← Подписка на кнопку Back
    }

    private void HandleStartGame()
    {
        if (!string.IsNullOrEmpty(startSceneName))
        {
            SceneManager.LoadScene(startSceneName);
        }
        else
        {
            Debug.LogError("Start scene name is not assigned.");
        }
    }

    private void HandleOpenQuiz()
    {
        if (!string.IsNullOrEmpty(quizSceneName))
        {
            SceneManager.LoadScene(quizSceneName);
        }
        else
        {
            Debug.LogError("Quiz scene name is not assigned.");
        }
    }
    private void ResetButtonAnimator(Button button)
    {
        if (button != null && button.TryGetComponent<Animator>(out var anim))
        {
            anim.Rebind(); // Сбрасывает все параметры и возвращает в default
            anim.Update(0); // Обновляет сразу
        }
    }

    private void HandleOpenOptions()
    {
        ResetButtonAnimator(optionsButton); // сбрасываем options кнопку
        buttonsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }


    private void HandleBackToMenu()
    {
        ResetButtonAnimator(backButton); // сбрасываем кнопку back
        optionsPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }



    private void HandleExitGame()
    {
        Debug.Log("Exiting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
