using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneOrientationManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneOrientation
    {
        public string sceneName;
        public ScreenOrientation orientation;
    }

    public List<SceneOrientation> sceneOrientations;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var so in sceneOrientations)
        {
            if (so.sceneName == scene.name)
            {
                Screen.orientation = so.orientation;
                Debug.Log($"Установлена ориентация: {so.orientation} для сцены {scene.name}");
                break;
            }
        }
    }
}
