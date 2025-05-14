using UnityEngine;

public class DiscardState : BlackJackState
{
    [SerializeField] private BetOrPassState betOrPassState;
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
     //   stateMachine.Context.DiscardPlayerCard(OnCompleteDiscardPlayer);
    }

    private void OnCompleteDiscardPlayer()
    {
        stateMachine.Context.DiscardDealerCard(OnComplete);
    }

    private void OnComplete()
    {
        stateMachine.SwitchState(betOrPassState);
    }

    public override void ExitState()
    {
    }
}