using System;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private BetMenuUI betMenuUI;
    [SerializeField] private PlayMenuUI playMenuUI;
    public event Action<PlayerChoice> OnPlayerChoice;
    public event Action<int> OnBet;

    private void OnEnable()
    {
        playMenuUI.OnPlayerChoice += PlayerChoice;
        betMenuUI.OnBet += Bet;
    }


    private void OnDisable()
    {
        betMenuUI.OnBet -= Bet;
        playMenuUI.OnPlayerChoice -= PlayerChoice;
    }


    private void Bet(int bet)
    {
        OnBet?.Invoke(bet);
    }

    private void PlayerChoice(PlayerChoice choice)
    {
        OnPlayerChoice?.Invoke(choice);
    }

    public void ShowPlayMenuUI(bool show)
    {
        playMenuUI.ShowUI(show);
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
        playMenuUI.ShowUI(false);
        betMenuUI.ShowUI(false);
    }
}