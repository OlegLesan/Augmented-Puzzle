using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ARPostDarknessOverlay : MonoBehaviour
{
    [Range(0f, 1f)] public float darknessAlpha = 0.85f;
    public LayerMask excludeLayer;

    private Material mat;

    void Start()
    {
        Shader shader = Shader.Find("Unlit/Color");
        mat = new Material(shader);
        mat.color = new Color(0f, 0f, 0f, darknessAlpha);
    }

    void OnPostRender()
    {
        if (mat == null) return;

        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.QUADS);
        GL.Color(new Color(0f, 0f, 0f, darknessAlpha));

        GL.Vertex3(0, 0, 0);
        GL.Vertex3(1, 0, 0);
        GL.Vertex3(1, 1, 0);
        GL.Vertex3(0, 1, 0);

        GL.End();
        GL.PopMatrix();
    }
}
