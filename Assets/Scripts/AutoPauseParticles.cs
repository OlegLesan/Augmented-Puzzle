using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(ParticleSystem))]
public class AutoPauseParticles : MonoBehaviour
{
    [Tooltip("„ерез сколько секунд поставить систему на паузу")]
    public float pauseAfterSeconds = 1f;

    private ParticleSystem ps;
    private float timer;
    private bool isPaused = false;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        timer = 0f;
        isPaused = false;

        if (!Application.isPlaying)
        {
            ps.Play();
            ps.Emit(1000); // ћожешь заменить на любое количество
            ps.Simulate(0f, true, false);
            ps.Pause();
            isPaused = true;
        }
    }

    void Update()
    {
        if (!Application.isPlaying || isPaused)
            return;

        timer += Time.deltaTime;
        if (timer >= pauseAfterSeconds)
        {
            ps.Pause();
            isPaused = true;
        }
    }
}