using System;
using System.Collections;
using RedDevil.PlayingCards;
using UnityEngine;

public class SelectDealerState : BlackJackState
{
    [SerializeField, Range(0, 5)] private int startPlayer;
    [SerializeField] private PotBetState potBetState;
    private bool AI;
    [SerializeField] private float waitTime = 3;
    private IEnumerator waitForDiscard;

    public override void EnterState()
    {
        stateMachine.Context.SetAllPlayerStyle();
        AI = AppData.gameType == GameType.AI;
    }

    public override void UpdateState()
    {
        if (AI)
        {
            var deck = CardSystem.GenerateDeck(false);
            deck.Shuffle();
            PlaceCard(deck);
        }
        else
        {
            SetDealer();
        }
    }

    private void PlaceCard(Deck deck)
    {
        stateMachine.Context.PlacePlayerCardAllCustom(deck, (completed) =>
        {
            if (completed)
            {
                PlaceCard(deck);
            }
            else
            {
                CheckHighCard();
            }
        }, true);
    }

    private void CheckHighCard()
    {
        BlackJackPlayer highestScoringPlayer = null;
        var highestScore = 0;
        foreach (var player in stateMachine.Context.currentPlayers)
        {
            var score = player.Score;
            if (score <= highestScore) continue;
            highestScore = score;
            highestScoringPlayer = player;
        }

        if (highestScoringPlayer == null)
        {
            startPlayer = 3;
        }
        else
        {
            highestScoringPlayer.blackJackPlayerUI.HighlightCard(true);
            startPlayer = BlackJackGameUtility.GetPlayerIndex(highestScoringPlayer.playerPosition);
            stateMachine.Context.ShowInfoForce("HighCard");
        }

        waitForDiscard = Wait(() =>
        {
            highestScoringPlayer?.blackJackPlayerUI.HighlightCard(false);
            stateMachine.Context.DiscardAllPlayerCard(SetDealer);
        
        });
        StartCoroutine(waitForDiscard);
    }

    private IEnumerator Wait(Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
        StopCoroutine(waitForDiscard);
    }

    public override void ExitState()
    {
        stateMachine.Context.cardPlaceCounter = 0;
        stateMachine.Context.cardRemoveCounter = 0;
        stateMachine.Context.passCounter = 0;
        startPlayer = 0;
        AI=false;
    }

    private void SetDealer()
    {
        if (stateMachine.Context.FirstGame)
        {
            stateMachine.Context.SetFirstGame(false);
            stateMachine.Context.SetPlayerCounter(startPlayer);
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.SetDealer(player, startPlayer);
            stateMachine.SwitchState(potBetState);
        }
        else
        {
            var counter = stateMachine.Context.PlayerCounter + 1;
            stateMachine.Context.SetPlayerCounter(counter);
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.SetDealer(player, counter);
            stateMachine.SwitchState(potBetState);
        }
    }
}