using UnityEngine;

public class CheckWinState : BlackJackState
{
    [SerializeField] private DiscardState discardState;
    private BlackJackPlayer player;
    private BlackJackPlayer dealer;

    public override void EnterState()
    {
        dealer = stateMachine.Context.Dealer;
        player = stateMachine.Context.GetCurrentPlayer();
        var amount = dealer.BetAmount - player.BetAmount;
        
        if (dealer.GetStatus() == PlayerStatus.Busted)
        {
            dealer.UpdateBetAmount(amount);
        }

        if (player.GetStatus() == PlayerStatus.Busted)
        {
            dealer.UpdateBetAmount(player.BetAmount + dealer.BetAmount);
        }
    }

    public override void UpdateState()
    {
        stateMachine.SwitchState(discardState);
    }

    public override void ExitState()
    {
    }
}