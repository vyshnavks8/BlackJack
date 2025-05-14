using System;
using RedDevil.PlayingCards;
using UnityEngine;
using UnityEngine.Serialization;

public class InitialCardPlaceState : BlackJackState
{
    private bool deckInit;
    [SerializeField] private BetOrPassState betOrPassState;
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
        PlaceCards(true, FinishedRound);
    }

    public override void ExitState()
    {
        stateMachine.Context.cardPlaceCounter = 0;
        RemoveListener();
    }

    private void FinishedRound()
    {
        stateMachine.SwitchState(betOrPassState);
    }


    private void PlaceCards(bool showDealer, Action callback)
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
            //Network
        }
        
        stateMachine.Context.PlacePlayerCard(completed => OnCompleted(completed, showDealer, callback), true, showCard);
    }

    private void OnCompleted(bool completed, bool showDealer, Action callback)
    {
        if (completed)
        {
            PlaceCards(showDealer, callback);
        }
        else
        {
            stateMachine.Context.PlaceDealerCard(showDealer, _ => callback?.Invoke());
          
        }
    }
}