using System;
using RedDevil.PlayingCards;
using UnityEngine;

public class PlayersCardPlaceState : BlackJackState
{
    [SerializeField] private DealerCardPlaceState dealerCardPlaceState;
    private bool deckInit;
    public override void AddListener()
    {
        stateMachine.NetworkEventSender.OnInitDeck += OnInitDeck;
    }


    public override void RemoveListener()
    {
        stateMachine.NetworkEventSender.OnInitDeck -= OnInitDeck;
    }

    private void OnInitDeck(Deck deck)
    {
        SetDeck(deck);
        UpdateState();
    }

    private void SetDeck(Deck deck)
    {
        stateMachine.Context.deck = deck;
        deckInit = true;
    }

    public override void EnterState()
    {
        AddListener();
        if (AppData.gameType == GameType.AI)
        {
            var deck = CardSystem.GenerateDeck(false);
            deck.Shuffle();
            SetDeck(deck);
        }
        else
        {
            if (!NetworkManager.IsMasterClient) return;
            var deck = CardSystem.GenerateDeck(false);
            deck.Shuffle();
            stateMachine.NetworkEventSender.InitDeck(deck);
        }
    }

    public override void UpdateState()
    {
        if (!deckInit) return;
        stateMachine.Context.IncrementNextPlayer();
        PlaceCards(FinishedRound);
    }

    public override void ExitState()
    {
        deckInit=false;
        stateMachine.Context.cardPlaceCounter = 0;
        RemoveListener();
    }

    private void FinishedRound()
    {
        stateMachine.SwitchState(dealerCardPlaceState);
    }


    private void PlaceCards(Action callback)
    {
        var showCard = false;
        if (AppData.gameType == GameType.AI)
        {
            if (!stateMachine.Context.IsCurrentPlayerBot)
            {
                showCard = true;
            }
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                showCard = true;
            }
        }
        
        stateMachine.Context.PlacePlayerCard(completed => OnCompleted(completed, callback), true, showCard);
    }

    private void OnCompleted(bool completed, Action callback)
    {
        if (completed)
        {
            PlaceCards(callback);
        }
        else
        {
            callback?.Invoke();
        }
    }
}