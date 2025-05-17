using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BetOverlayCanvas : OverlayCanvas
{
    [SerializeField] private Button enterButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private UnityEvent<int> onBetValueChanged;

    protected override void AddListener()
    {
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
        enterButton.onClick.AddListener(OnEnterClick);
    }


    protected override void RemoveListener()
    {
        inputField.onEndEdit.RemoveListener(OnInputFieldEndEdit);
        enterButton.onClick.RemoveListener(OnEnterClick);
    }

    private void OnInputFieldEndEdit(string value)
    {
        var i = int.Parse(value);
        onBetValueChanged?.Invoke(i);
    }

    private void OnEnterClick()
    {
        HideOverlay();
        inputField.text = string.Empty;
    }
}