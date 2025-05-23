using UnityEngine;
using Vuforia;

public class ARDarknessTrigger : MonoBehaviour
{
    [Range(0f, 1f)] public float darknessAlpha = 0.85f;

    private GameObject darknessSphere;
    private bool effectApplied = false;

    void Start()
    {
        var observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (!effectApplied && (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED))
        {
            ApplyDarkness();
            effectApplied = true;
        }
    }

    void ApplyDarkness()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogError("❌ Не найдена камера с тегом MainCamera!");
            return;
        }

        // создаём огромную полупрозрачную сферу вокруг камеры
        darknessSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(darknessSphere.GetComponent<Collider>());

        darknessSphere.name = "DarknessSphere";
        darknessSphere.transform.SetParent(mainCam.transform);
        darknessSphere.transform.localPosition = Vector3.zero;
        darknessSphere.transform.localScale = Vector3.one * 10f;

        // создаём чёрный прозрачный материал
        Material darkMat = new Material(Shader.Find("Custom/DarkTransparent"));
        darkMat.color = new Color(0f, 0f, 0f, darknessAlpha);

        darknessSphere.GetComponent<MeshRenderer>().material = darkMat;

        Debug.Log("🌑 Затемнение включено!");
    }
}
