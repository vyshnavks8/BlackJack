using System;
using System.Collections;
using System.Linq;
using RedDevil.PlayingCards;
using UnityEngine;
using Random = UnityEngine.Random;


public class BlackJackBotManager : MonoBehaviour
{
    [SerializeField] private ChipDataSO chipDataSO;
    private IEnumerator timer;

    public void DoBet(BlackJackPlayer blackJackPlayer, Action<int> bet)
    {
        var rand = Random.Range(0, chipDataSO.Count);
        var chipAmount = chipDataSO.GetChipAmount(rand);
        var betTime = Random.Range(0, blackJackPlayer.TimeAllowed / 2);
        timer = DecisionTimer(betTime, () => bet?.Invoke(chipAmount));
        StartCoroutine(timer);
    } 
    public void DoPlaySelect(BlackJackPlayer blackJackPlayer,Action<PlayerChoice> playerChoice)
    {
        var choice = PlayerChoice.None;
        var rand = Random.Range(0, 2);
        choice = rand switch
        {
            0 => PlayerChoice.Pass,
            1 => PlayerChoice.Bet,
            _ => choice
        };
        var betTime = Random.Range(0, blackJackPlayer.TimeAllowed / 2);
        timer = DecisionTimer(betTime, () => playerChoice?.Invoke(PlayerChoice.Pass));
        StartCoroutine(timer);
    }
    public void DoPlayChoice(BlackJackPlayer dealer, BlackJackPlayer blackJackPlayer, Action<PlayerChoice> playerChoice)
    {
        var dealerCard = dealer.GetTopCard();
        var choice = GetChoice(dealerCard, blackJackPlayer);
        var playTime = Random.Range(0, blackJackPlayer.TimeAllowed / 2);
        timer = DecisionTimer(playTime, () => playerChoice?.Invoke(choice));
        StartCoroutine(timer);
    }

    public void ResetBot()
    {
        if(timer == null) return;
        StopCoroutine(timer);
    }

    private IEnumerator DecisionTimer(float decisionTime, Action completed)
    {
        yield return new WaitForSeconds(decisionTime);
        completed?.Invoke();
    }

   
    private PlayerChoice GetChoice(Card dealerCard, BlackJackPlayer blackJackPlayer)
    {
        var dealerScore = BlackJackValidator.GetCardValue(dealerCard);

        if (blackJackPlayer.IsSoftTotal())
        {
            var score = blackJackPlayer.cards.Sum(BlackJackValidator.GetCardValue);
            return AIBotUtility.GetSoftChoice(dealerScore,score);
        }

        return AIBotUtility.GetHardChoice(dealerScore, blackJackPlayer.Score);
    }
}