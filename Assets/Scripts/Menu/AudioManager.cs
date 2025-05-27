using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public bool IsMuted { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleSound()
    {
        IsMuted = !IsMuted;
        ApplyMute();
    }

    public void ApplyMute()
    {
        AudioListener.volume = IsMuted ? 0f : 1f;

        // Mute AudioSources
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.mute = IsMuted;
        }

        // Mute VideoPlayers
        foreach (var vp in FindObjectsOfType<VideoPlayer>())
        {
            int trackCount = vp.audioTrackCount;

            for (ushort i = 0; i < trackCount; i++)
            {
                vp.SetDirectAudioMute(i, IsMuted);
            }

            // если у него есть AudioSource, тоже мьютим
            if (vp.GetTargetAudioSource(0) != null)
            {
                vp.GetTargetAudioSource(0).mute = IsMuted;
            }
        }
    }
}
