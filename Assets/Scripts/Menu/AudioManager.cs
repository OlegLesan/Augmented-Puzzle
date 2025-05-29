using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mute")]
    [SerializeField] private bool _isMuted = false;
    public bool IsMuted => _isMuted;

    [Header("SFX List")]
    public List<AudioClip> sfxClips = new List<AudioClip>();

    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
    }

    public void ToggleSound()
    {
        _isMuted = !_isMuted;
        ApplyMute();
    }

    public void ApplyMute()
    {
        AudioListener.volume = _isMuted ? 0f : 1f;

        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.mute = _isMuted;
        }

        foreach (var vp in FindObjectsOfType<VideoPlayer>())
        {
            int trackCount = vp.audioTrackCount;

            for (ushort i = 0; i < trackCount; i++)
            {
                vp.SetDirectAudioMute(i, _isMuted);
            }

            if (vp.GetTargetAudioSource(0) != null)
            {
                vp.GetTargetAudioSource(0).mute = _isMuted;
            }
        }
    }

    public void PlaySFXByIndex(int index)
    {
        if (_isMuted || sfxClips == null || index < 0 || index >= sfxClips.Count)
            return;

        AudioClip clip = sfxClips[index];
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
