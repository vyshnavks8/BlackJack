using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private float infoTime=0.2f;
    [SerializeField] private Image bg;
    [SerializeField] private BetMenuUI betMenuUI;
    [SerializeField] private PlayChoiceMenuUI playHitStandMenuUI;
    [SerializeField] private PlayChoiceMenuUI playBetPassMenuUI;
    [SerializeField] private PlayChoiceMenuUI playShowMuckMenuUI;
    [SerializeField] private BetOverlayCanvas betOverlayCanvas;
    public event Action<PlayerChoice> OnPlayerChoice;
    public event Action<int> OnBet;

    private void OnEnable()
    {
        playHitStandMenuUI.OnPlayerChoice += PlayerChoice;
        playBetPassMenuUI.OnPlayerChoice += PlayerChoice;
        playShowMuckMenuUI.OnPlayerChoice += PlayerChoice;
        betMenuUI.OnBet += Bet;
    }


    private void OnDisable()
    {
        playHitStandMenuUI.OnPlayerChoice -= PlayerChoice;
        playBetPassMenuUI.OnPlayerChoice -= PlayerChoice;
        playShowMuckMenuUI.OnPlayerChoice -= PlayerChoice;
        betMenuUI.OnBet -= Bet;
    }


    private void Bet(int bet)
    {
        OnBet?.Invoke(bet);
    }

    private void PlayerChoice(PlayerChoice choice)
    {
        OnPlayerChoice?.Invoke(choice);
    }

    public void ShowHitStandMenuUI(bool show)
    {
        playHitStandMenuUI.ShowUI(show);
        ShowBG(show);
    }

    public void ShowBetPassMenuUI(bool show)
    {
        playBetPassMenuUI.ShowUI(show);
        ShowBG(show);
    }

    public void ShowMuckMenuUI(bool show)
    {
        playShowMuckMenuUI.ShowUI(show);
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

    public void ShowInfoText(string info,Action onComplete=null)
    {
        infoText.text = info;
        infoText.gameObject.SetActive(true);
        var infoTextEnumerator = StartTimer(onComplete);
        StartCoroutine(infoTextEnumerator);
    }

    private IEnumerator StartTimer(Action callback)
    {
        yield return new WaitForSeconds(infoTime);
        infoText.gameObject.SetActive(false);
        infoText.text = string.Empty;
        callback?.Invoke();
    }

    public void HideUI()
    {
        ShowBG(false);
        playHitStandMenuUI.ShowUI(false);
        playBetPassMenuUI.ShowUI(false);
        betMenuUI.ShowUI(false);
    }

    public void HideBetOverlay()
    {
        betOverlayCanvas.Hide();
    }
}