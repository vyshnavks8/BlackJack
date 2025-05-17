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

    public void DoBetAmount(BlackJackPlayer blackJackPlayer, int amount, Action<int> bet)
    {
        var maxInclusive = amount == 0 ? chipDataSO.Count : chipDataSO.GetIDBelow(amount);
        var id = Random.Range(0, maxInclusive);
        var chipAmount = chipDataSO.GetChipAmount(id);
        var betTime = Random.Range(0, blackJackPlayer.TimeAllowed / 2);
        timer = DecisionTimer(betTime, () => bet?.Invoke(chipAmount));
        StartCoroutine(timer);
    }

    public void DoBetOrPass(BlackJackPlayer player, Action<PlayerChoice> playerChoice)
    {
        var choice = PlayerChoice.None;
        var rand = Random.Range(0, 2);
        choice = rand switch
        {
            0 => PlayerChoice.Pass,
            1 => PlayerChoice.Bet,
            _ => choice
        };
        var decisionTime = Random.Range(0, player.TimeAllowed / 2);
        timer = DecisionTimer(decisionTime, () => playerChoice?.Invoke(PlayerChoice.Bet));
        StartCoroutine(timer);
    }

    public void DoShowOrMuck(BlackJackPlayer dealer, BlackJackPlayer player, Action<PlayerChoice> playerChoice)
    {
        var decisionTime = Random.Range(0, player.TimeAllowed / 2);
        var choice = dealer.Score > player.Score ? PlayerChoice.Muck : PlayerChoice.Show;
        timer = DecisionTimer(decisionTime, () => playerChoice?.Invoke(choice));
        StartCoroutine(timer);
    }

    public void DoHitOrStand(BlackJackPlayer dealer, BlackJackPlayer player, Action<PlayerChoice> playerChoice)
    {
        var dealerCard = dealer.GetTopCard();
        var choice = GetHitOrStandChoice(dealerCard, player);
        var decisionTime = Random.Range(0, player.TimeAllowed / 2);
        timer = DecisionTimer(decisionTime, () => playerChoice?.Invoke(choice));
        StartCoroutine(timer);
    }

    public void ResetBot()
    {
        if (timer == null) return;
        StopCoroutine(timer);
    }

    private IEnumerator DecisionTimer(float decisionTime, Action completed)
    {
        yield return new WaitForSeconds(decisionTime);
        completed?.Invoke();
    }


    private PlayerChoice GetHitOrStandChoice(Card dealerCard, BlackJackPlayer blackJackPlayer)
    {
        var dealerScore = BlackJackValidator.GetCardValue(dealerCard);

        if (blackJackPlayer.IsSoftTotal())
        {
            var score = blackJackPlayer.cards.Sum(BlackJackValidator.GetCardValue);
            return AIBotUtility.GetSoftChoice(dealerScore, score);
        }

        return AIBotUtility.GetHardChoice(dealerScore, blackJackPlayer.Score);
    }
}