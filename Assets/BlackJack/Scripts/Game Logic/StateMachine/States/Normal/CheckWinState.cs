using UnityEngine;

public class CheckWinState : BlackJackState
{
    [SerializeField] private DiscardState discardState;
    [SerializeField] private ShowOrMuckState showOrMuckState;
    private BlackJackPlayer player;
    private BlackJackPlayer dealer;

    public override void EnterState()
    {
        dealer = stateMachine.Context.Dealer;
        player = stateMachine.Context.GetCurrentPlayer();
    }

    private void OnComplete()
    {
        stateMachine.SwitchState(discardState);
    }

    public override void UpdateState()
    {
        if (CheckPlayerWinner()) return;
        if (CheckDealerWinner()) return;
        stateMachine.SwitchState(showOrMuckState);
    }

    private bool CheckDealerWinner()
    {
        var status = player.GetStatus() == PlayerStatus.Busted || dealer.GetStatus() == PlayerStatus.Won;
        if (status)
        {
            stateMachine.Context.MakeDealerWinner(dealer,player,OnComplete);
        }
        return status;
    }

    private bool CheckPlayerWinner()
    {
        var status = dealer.GetStatus() == PlayerStatus.Busted || player.GetStatus() == PlayerStatus.Won;
        if (status)
        {
            stateMachine.Context.MakePlayerWinner(dealer,player,OnComplete);
        }
        return status;
    }

    public override void ExitState()
    {
    }
}