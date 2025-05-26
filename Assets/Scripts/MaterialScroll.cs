using UnityEngine;

public class MaterialScroll : MonoBehaviour
{
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.0f;

    private Renderer rend;
    private Vector2 currentOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        currentOffset = rend.material.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        currentOffset.x += scrollSpeedX * Time.deltaTime;
        currentOffset.y += scrollSpeedY * Time.deltaTime;

        rend.material.SetTextureOffset("_MainTex", currentOffset);
    }
}
