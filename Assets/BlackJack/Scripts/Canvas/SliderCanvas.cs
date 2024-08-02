using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderCanvas : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text percentageText;
    private void OnEnable()
    {
        OnSliderValue(slider.value);
        slider.onValueChanged.AddListener(OnSliderValue);
    }

    private void OnDisable()
    {
        OnSliderValue(slider.value);
        slider.onValueChanged.RemoveListener(OnSliderValue);
    }

    private void OnSliderValue(float value)
    {
        var percentage = value * 100;
        percentageText.text =  (int)percentage + " %";
    }
}