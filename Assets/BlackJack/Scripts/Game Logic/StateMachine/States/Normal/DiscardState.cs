using System;
using System.Collections;
using UnityEngine;

public class DiscardState : BlackJackState
{
    [SerializeField] private DealerCardPlaceState dealerCardPlace;
    [SerializeField] private FinishState finishState;
    [SerializeField] private float waitTime = 2;
    private BlackJackPlayer player;
    private IEnumerator waitForDiscard;

    public override void EnterState()
    {
        player = stateMachine.Context.GetCurrentPlayer();
    }

    public override void UpdateState()
    {
        player.HideStatus();
        player.ClearCards();
        waitForDiscard = Wait(() => { stateMachine.Context.DiscardPlayerCard(OnCompleteDiscardPlayer); });
        StartCoroutine(waitForDiscard);
    }

    private IEnumerator Wait(Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
        StopCoroutine(waitForDiscard);
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