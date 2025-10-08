using System;
using UnityEngine;
using UnityEngine.UI;


public class BlackJackPlayerProfileUI : MonoBehaviour
{
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite botSprite;
    [SerializeField] private CoolDownTimerUI timerUI;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image bgImage;
    [SerializeField] private Color dealerColor;
    [SerializeField] private Color playerColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private Color enabledColor;
    [SerializeField] private GameObject playerIconPivot;
    [SerializeField] private Image playerIcon;

    private float time;

    private void OnDisable()
    {
        SetIcon(null);
    }

    public void SetData(PlayerType playerType, float timer)
    {
        time = timer;
        switch (playerType)
        {
            case PlayerType.None:
                break;
            case PlayerType.Bot:
                iconImage.sprite = botSprite;
                break;
            case PlayerType.Player:
                iconImage.sprite = playerSprite;
                break;
        }
    }

    public void SetIcon(Sprite sprite)
    {
        playerIcon.sprite = sprite;
        playerIconPivot.SetActive(sprite != null);
    }

    public void StartTurn(Action callback)
    {
        timerUI.StartCooldownTimer(time, () =>
        {
            callback?.Invoke();
            OnFinishTurn();
        });
    }

    public void DisablePlayer()
    {
        bgImage.color = disabledColor;
    }

    public void EnablePlayer()
    {
        bgImage.color = enabledColor;
    }

    public void StopTurn()
    {
        OnFinishTurn();
    }

    public void SetDealerStyle()
    {
        bgImage.color = dealerColor;
    }

    public void SetPlayerStyle()
    {
        bgImage.color = playerColor;
    }

    private void OnFinishTurn()
    {
        timerUI.StopCooldownTimer();
    }
}