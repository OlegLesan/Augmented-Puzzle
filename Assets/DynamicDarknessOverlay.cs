using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class DynamicDarkness : MonoBehaviour
{
    public RawImage overlayImage;
    public Transform imageTarget;
    public float fadeRadius = 0.25f; // радиус "света" вокруг ImageTarget (в долях экрана)

    private Texture2D maskTexture;

    void Start()
    {
        maskTexture = new Texture2D(512, 512);
        maskTexture.wrapMode = TextureWrapMode.Clamp;
        maskTexture.filterMode = FilterMode.Bilinear;

        overlayImage.texture = maskTexture;
    }

    void Update()
    {
        if (imageTarget == null || Camera.main == null)
            return;

        // Получаем экранную позицию ImageTarget
        Vector3 screenPos = Camera.main.WorldToViewportPoint(imageTarget.position);

        for (int y = 0; y < maskTexture.height; y++)
        {
            for (int x = 0; x < maskTexture.width; x++)
            {
                // Преобразуем координаты пикселя в нормализованный [0,1]
                float u = (float)x / maskTexture.width;
                float v = (float)y / maskTexture.height;

                float dist = Vector2.Distance(new Vector2(u, v), new Vector2(screenPos.x, screenPos.y));
                float alpha = Mathf.Clamp01((dist - fadeRadius) * 5f); // чем дальше от центра — тем темнее

                maskTexture.SetPixel(x, y, new Color(0, 0, 0, alpha));
            }
        }

        maskTexture.Apply();
    }
}
