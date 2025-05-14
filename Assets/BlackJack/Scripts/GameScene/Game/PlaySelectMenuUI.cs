using System;
using UnityEngine;
using UnityEngine.UI;

public class PlaySelectMenuUI : MonoBehaviour
{
    [SerializeField] private Button betButton;
    [SerializeField] private Button passButton;
    [SerializeField] private RectTransform pivot;
    public event Action<PlayerChoice> OnPlayerChoice;


    private void OnEnable()
    {
        betButton.onClick.AddListener(OnClickBetButton);
        passButton.onClick.AddListener(OnClickPassButton);
    }

    private void OnDisable()
    {
        betButton.onClick.RemoveListener(OnClickBetButton);
        passButton.onClick.RemoveListener(OnClickPassButton);
    }

    public void ShowUI(bool show)
    {
        pivot.gameObject.SetActive(show);
    }

    private void OnClickPassButton()
    {
        OnPlayerChoice?.Invoke(PlayerChoice.Pass);
    }


    private void OnClickBetButton()
    {
        OnPlayerChoice?.Invoke(PlayerChoice.Bet);
    }
}