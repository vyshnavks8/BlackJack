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
        PlaceCards(false, FinishedRound);
    }

    public override void ExitState()
    {
        RemoveListener();
    }

    private void FinishedRound()
    {
        stateMachine.SwitchState(playState);
    }
    

    private void PlaceCards(bool showDealer, Action callback)
    {
        stateMachine.Context.PlacePlayerCard(_ => OnCompleted(showDealer, callback));
    }

    private void OnCompleted(bool showDealer, Action callback)
    {
        stateMachine.Context.PlaceDealerCard(showDealer, _ => callback?.Invoke());
    }
}