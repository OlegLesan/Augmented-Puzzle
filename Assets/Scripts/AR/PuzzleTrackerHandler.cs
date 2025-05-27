using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class PuzzleTrackerHandler : MonoBehaviour
{
    public GameObject videoObject;         // Quad с VideoPlayer
    public GameObject rotationTarget;      // Родитель видео с RotationVideo

    private VideoPlayer videoPlayer;
    private Animator animator;
    private RotationVideo rotationScript;
    private ObserverBehaviour observerBehaviour;

    private bool hasPlayed = false;

    void Start()
    {
        videoPlayer = videoObject.GetComponent<VideoPlayer>();
        videoPlayer.enabled = false;

        animator = GetComponent<Animator>();
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (rotationTarget != null)
        {
            rotationScript = rotationTarget.GetComponent<RotationVideo>();
            if (rotationScript != null)
                rotationScript.enabled = false;
        }

        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (!hasPlayed && status.Status == Status.TRACKED)
        {
            hasPlayed = true;
            TriggerAppearance();
        }
    }

    private void TriggerAppearance()
    {
        animator.SetTrigger("StartVideo");
    }

    // Вызывается из анимации (Animation Event)
    public void OnAnimationEnd()
    {
        if (rotationScript != null)
            rotationScript.enabled = true;

        videoPlayer.enabled = true;
        videoPlayer.Play();
    }
}
