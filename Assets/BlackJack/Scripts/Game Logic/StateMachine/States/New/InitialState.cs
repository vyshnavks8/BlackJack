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
        var showDealer = false;
        if (AppData.gameType == GameType.AI)
        {
            showDealer = stateMachine.Context.Dealer.PlayerType == PlayerType.Player;
        }
        else
        {
            var player = stateMachine.Context.Dealer;
            if (player.IsLocalNetworkPlayer())
            {
                showDealer = true;
            }
        }

        stateMachine.Context.PlaceDealerCard(showDealer, _ => callback?.Invoke());
    }
}