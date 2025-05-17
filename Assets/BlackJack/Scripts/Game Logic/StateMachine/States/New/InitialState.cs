using System;
using UnityEngine;

public class InitialState : BlackJackState
{
    [SerializeField] private PlayState playState;

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        PlaceCards(FinishedRound);
    }

    public override void ExitState()
    {
        RemoveListener();
    }

    private void FinishedRound()
    {
        stateMachine.SwitchState(playState);
    }


    private void PlaceCards(Action callback)
    {
        stateMachine.Context.PlacePlayerCard(_ => OnCompleted(callback));
    }

    private void OnCompleted(Action callback)
    {
        var showDealer = stateMachine.Context.Dealer.PlayerType==PlayerType.Player;
        stateMachine.Context.PlaceDealerCard(showDealer, _ => callback?.Invoke());
    }
}