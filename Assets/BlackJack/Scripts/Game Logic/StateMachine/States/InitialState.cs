using System;
using RedDevil.PlayingCards;

public class InitialState : BlackJackState
{
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
        PlaceCards(true, FinishedFirstRound);
    }

    public override void ExitState()
    {
        stateMachine.Context.playerCounter = 0;
        RemoveListener();
    }

    private void FinishedFirstRound()
    {
        stateMachine.Context.playerCounter = 0;
        PlaceCards(false, OnFinishSecondRound);
    }

    private void OnFinishSecondRound()
    {
        stateMachine.SwitchState();
    }

    private void PlaceCards(bool showDealer, Action callback)
    {
        stateMachine.Context.PlacePlayerCard(finished => OnCompleted(finished, showDealer, callback), true);
    }

    private void OnCompleted(bool finished, bool showDealer, Action callback)
    {
        if (finished)
        {
            PlaceCards(showDealer, callback);
        }
        else
        {
            stateMachine.Context.PlaceDealerCard(showDealer, _ => callback?.Invoke());
        }
    }
}