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
        public bool allowBothLandscapeSides; // ����� ����
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
                if (so.allowBothLandscapeSides && so.orientation == ScreenOrientation.LandscapeLeft)
                {
                    Screen.orientation = ScreenOrientation.AutoRotation;
                    Screen.autorotateToLandscapeLeft = true;
                    Screen.autorotateToLandscapeRight = true;

                    Screen.autorotateToPortrait = false;
                    Screen.autorotateToPortraitUpsideDown = false;

                    Debug.Log($"�������������� ���������� � ����� ������ ������� ��� ����� {scene.name}");
                }
                else
                {
                    Screen.orientation = so.orientation;
                    Debug.Log($"����������� ����������: {so.orientation} ��� ����� {scene.name}");
                }
                break;
            }
        }
    }
}
