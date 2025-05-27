using UnityEngine;
using UnityEngine.UI;

public class MuteButtonUI : MonoBehaviour
{
    public Button toggleButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = toggleButton.GetComponent<Image>();
        toggleButton.onClick.AddListener(OnToggleSound);

        // Установим правильный спрайт при старте
        UpdateSprite();
    }

    private void OnToggleSound()
    {
        AudioManager.Instance.ToggleSound();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = AudioManager.Instance.IsMuted ? soundOffSprite : soundOnSprite;
        }
    }
}
