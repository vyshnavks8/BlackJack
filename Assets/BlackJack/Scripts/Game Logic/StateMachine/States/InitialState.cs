using System;
using RedDevil.PlayingCards;

public class InitialState : BlackJackState
{
    public override void EnterState()
    {
        var deck = CardSystem.GenerateDeck(false);
        deck.Shuffle();
        stateMachine.Context.deck = deck;
    }

    public override void UpdateState()
    {
        PlaceCards(true, FinishedFirstRound);
    }

    public override void ExitState()
    {
        stateMachine.Context.playerCounter=0;
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
        stateMachine.Context.PlacePlayerCard(finished => OnCompleted(finished, showDealer, callback),true);
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