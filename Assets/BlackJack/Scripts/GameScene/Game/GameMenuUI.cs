using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private BetMenuUI betMenuUI;
    [SerializeField] private PlayChoiceMenuUI playChoiceMenuUI;
    [SerializeField] private PlaySelectMenuUI playSelectMenuUI;
    public event Action<PlayerChoice> OnPlayerChoice;
    public event Action<int> OnBet;

    private void OnEnable()
    {
        playChoiceMenuUI.OnPlayerChoice += PlayerChoice;
        playSelectMenuUI.OnPlayerChoice += PlayerChoice;
        betMenuUI.OnBet += Bet;
    }


    private void OnDisable()
    {
        betMenuUI.OnBet -= Bet;
        playChoiceMenuUI.OnPlayerChoice -= PlayerChoice;
        playSelectMenuUI.OnPlayerChoice -= PlayerChoice;
    }


    private void Bet(int bet)
    {
        OnBet?.Invoke(bet);
    }

    private void PlayerChoice(PlayerChoice choice)
    {
        OnPlayerChoice?.Invoke(choice);
    }

    public void ShowPlayChoiceMenuUI(bool show)
    {
        playChoiceMenuUI.ShowUI(show);
        ShowBG(show);
    }

    public void ShowPlaySelectMenuUI(bool show)
    {
        playSelectMenuUI.ShowUI(show);
        ShowBG(show);
    }

    public void ShowBetMenuUI(bool show)
    {
        betMenuUI.ShowUI(show);
        ShowBG(show);
    }

    private void ShowBG(bool show)
    {
        bg.gameObject.SetActive(show);
    }

    public void HideUI()
    {
        ShowBG(false);
        playChoiceMenuUI.ShowUI(false);
        playSelectMenuUI.ShowUI(false);
        betMenuUI.ShowUI(false);
    }
}