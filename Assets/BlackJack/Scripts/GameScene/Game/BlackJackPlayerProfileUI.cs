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

    private float time;

    public void SetData(PlayerType playerType, float timer)
    {
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
        time = timer;
        
    }
    public void StartTurn(Action callback)
    {
        timerUI.StartCooldownTimer(time,()=>
        {
            callback?.Invoke();
            OnFinishTurn();
        });
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