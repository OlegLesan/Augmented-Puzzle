using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerAR : MonoBehaviour
{
    // Название сцены, на которую переходим по первой кнопке
    [SerializeField] private string sceneToLoad;

    /// <summary>
    /// Загружает указанную сцену (переход на другую сцену).
    /// </summary>
    public void LoadOtherScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name is not set in ButtonManagerAR.");
        }
    }

    /// <summary>
    /// Перезапускает текущую сцену (сброс AR-трекинга).
    /// </summary>
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
