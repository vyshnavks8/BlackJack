using UnityEngine;
using UnityEngine.UI;

public class VolumeCanvas : CanvasBase
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Button backButton;
    private float music;
    private float effect;
    protected override void AddListener()
    {
        musicSlider.onValueChanged.AddListener(OnMusicToggle);
        effectSlider.onValueChanged.AddListener(OnEffectToggle);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicToggle);
        effectSlider.onValueChanged.RemoveListener(OnEffectToggle);
        backButton.onClick.RemoveListener(OnBackClick);
    }
    private void OnBackClick()
    {
        OnSetCanvasActive(transitionCanvas);
    }
    private void OnEffectToggle(float value)
    {
        effect = value;
    }

    private void OnMusicToggle(float value)
    {
        music = value;
    }
}