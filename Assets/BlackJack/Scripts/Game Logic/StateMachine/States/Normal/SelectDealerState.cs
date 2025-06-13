using UnityEngine;

public class SelectDealerState : BlackJackState
{
    [SerializeField, Range(0, 5)] private int startPlayer;
    [SerializeField] private PotBetState potBetState;

    public override void EnterState()
    {
        stateMachine.Context.SetAllPlayerStyle();
        if (AppData.gameType == GameType.AI)
        {
            startPlayer = 3;
        }
       
    }

    public override void UpdateState()
    {
        SetDealer();
    }

    public override void ExitState()
    {
    }

    private void SetDealer()
    {
        if (stateMachine.Context.FirstGame)
        {
            stateMachine.Context.SetFirstGame(false);
            stateMachine.Context.SetPlayerCounter(startPlayer);
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.SetDealer(player, startPlayer);
            stateMachine.SwitchState(potBetState);
        }
        else
        {
            var counter = stateMachine.Context.PlayerCounter + 1;
            stateMachine.Context.SetPlayerCounter(counter);
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.SetDealer(player, counter);
            stateMachine.SwitchState(potBetState);
        }
    }
}