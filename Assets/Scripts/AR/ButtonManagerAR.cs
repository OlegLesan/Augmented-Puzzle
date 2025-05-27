using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerAR : MonoBehaviour
{
    // �������� �����, �� ������� ��������� �� ������ ������
    [SerializeField] private string sceneToLoad;

    /// <summary>
    /// ��������� ��������� ����� (������� �� ������ �����).
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
    /// ������������� ������� ����� (����� AR-��������).
    /// </summary>
    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
