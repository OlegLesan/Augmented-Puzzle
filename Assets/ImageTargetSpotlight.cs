using UnityEngine;
using Vuforia;

public class ImageTargetSpotlight : MonoBehaviour
{
    private Light spotlight;
    public float intensity = 3f;
    public float range = 2f;

    void Start()
    {
        spotlight = gameObject.AddComponent<Light>();
        spotlight.type = LightType.Point;
        spotlight.intensity = intensity;
        spotlight.range = range;
        spotlight.color = Color.white;
        spotlight.shadows = LightShadows.None;
    }

    void Update()
    {
        // Убедимся, что свет всегда на позиции ImageTarget
        spotlight.transform.position = transform.position + Vector3.up * 0.1f;
    }
}
