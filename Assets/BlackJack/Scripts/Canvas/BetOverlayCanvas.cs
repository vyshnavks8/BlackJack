using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BetOverlayCanvas : OverlayCanvas
{
    [SerializeField] private Button enterButton;
   // [SerializeField] private Button enterButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private UnityEvent<int> onBetValueChanged;
    private int betValue;
    public event Action<int> OnBet;

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
        betValue = i;
        onBetValueChanged?.Invoke(i);
    }

    private void OnEnterClick()
    {
        if(betValue == 0) return;
        OnBet?.Invoke(betValue);
        Hide();
    }

    public void Show(bool valid)
    {
        if (valid)
        {
            gameObject.SetActive(true);
            ShowOverlay();
        }
        else
        {
            Hide();
        }
    }

    public void Hide()
    {
        HideOverlay();
        inputField.text = string.Empty;
    }
}