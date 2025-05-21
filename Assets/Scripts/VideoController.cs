using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private Animator animator;
    private ObserverBehaviour observerBehaviour;

    private bool hasPlayedAnimation = false;
    private bool videoStarted = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        animator = GetComponent<Animator>();
        observerBehaviour = GetComponentInParent<ObserverBehaviour>();

        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        if (videoPlayer)
        {
            videoPlayer.playOnAwake = false;
            videoPlayer.Stop();
        }
    }

    private void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if ((targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED) && !hasPlayedAnimation)
        {
            PlayAnimation();
        }

        // Видео больше не останавливаем при потере трекинга
        // else if (targetStatus.Status == Status.NO_POSE)
        // {
        //     ResetState();
        // }
    }

    private void PlayAnimation()
    {
        if (animator)
        {
            animator.SetTrigger("PlayAwake");
            hasPlayedAnimation = true;

            float animationLength = animator.runtimeAnimatorController.animationClips[0].length;
            Invoke(nameof(PlayVideo), animationLength);
        }
        else
        {
            PlayVideo();
        }
    }

    private void PlayVideo()
    {
        if (videoPlayer && !videoPlayer.isPlaying && !videoStarted)
        {
            videoPlayer.Play();
            videoStarted = true;
        }
    }

    private void ResetState()
    {
        // Ничего не сбрасываем кроме анимации, если хочешь повторно играть
        hasPlayedAnimation = false;
        // videoStarted остаётся true
        // videoPlayer не останавливаем
    }
}
