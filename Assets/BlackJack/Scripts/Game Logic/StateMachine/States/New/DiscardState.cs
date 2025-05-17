using UnityEngine;

public class DiscardState : BlackJackState
{
    [SerializeField] private DealerCardPlaceState dealerCardPlace;
    [SerializeField] private FinishState finishState;

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        var player = stateMachine.Context.GetCurrentPlayer();
        player.HideStatus();
        player.ClearCards();
        stateMachine.Context.DiscardPlayerCard(OnCompleteDiscardPlayer);
    }

    private void OnCompleteDiscardPlayer()
    {
        stateMachine.Context.Dealer.HideStatus();
        stateMachine.Context.Dealer.ClearCards();
        stateMachine.Context.DiscardDealerCard(OnComplete);
    }

    private void OnComplete()
    {
        if (stateMachine.Context.Dealer.BetAmount == 0)
        {
            stateMachine.Context.ShowInfo("POT IS EMPTY");
            stateMachine.SwitchState(finishState);
            return;
        }

        if (stateMachine.Context.IsMaxPlayerReached)
        {
            stateMachine.Context.ShowInfo("ROUND FINISHED   ");
            stateMachine.SwitchState(finishState);
            return;
        }

        stateMachine.SwitchState(dealerCardPlace);
    }

    public override void ExitState()
    {
    }
}