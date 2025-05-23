using UnityEngine;
using Vuforia;

public class ARDarknessTrigger_OnlyOtherLayers : MonoBehaviour
{
    [Range(0f, 1f)] public float darknessAlpha = 0.85f;

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
        Debug.Log("🎯 ImageTarget найден, затемнение запускается");

    }

    void ApplyDarkness()
    {
        GameObject darkQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Destroy(darkQuad.GetComponent<Collider>());

        darkQuad.name = "DarknessScreen";
        darkQuad.transform.SetParent(Camera.main.transform);
        darkQuad.transform.localPosition = new Vector3(0f, 0f, 0.01f); // СОВСЕМ ЧУТЬ ВПЕРЕД
        darkQuad.transform.localRotation = Quaternion.identity;
        darkQuad.transform.localScale = new Vector3(100f, 100f, 1f);

        Material darkMat = new Material(Shader.Find("Custom/DarkOnlyOtherLayers"));
        darkMat.color = new Color(0f, 0f, 0f, darknessAlpha);
        darkQuad.GetComponent<MeshRenderer>().material = darkMat;

        darkQuad.layer = LayerMask.NameToLayer("Darkness");
    }

}
