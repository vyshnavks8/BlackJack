using UnityEngine;

public class SelectDealerState : BlackJackState
{
   [SerializeField,Range(0,5)] private int startPlayer;
   [SerializeField] private PotBetState potBetState;
    public override void EnterState()
    {
        stateMachine.Context.SetAllPlayerStyle();
       SetDealer();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
    }
    private void SetDealer()
    {
        //stateMachine.Context.StartPlayerTimer(OnTimerFinishBet);
        //var bot = stateMachine.Context.CheckBotBet(OnBet);
        if (AppData.gameType == GameType.AI)
        {
            if (stateMachine.Context.FirstGame)
            {
                stateMachine.Context.SetFirstGame(false);
                stateMachine.Context.playerCounter = startPlayer;
                var player = stateMachine.Context.GetCurrentPlayer();
                stateMachine.Context.SetDealer(player,stateMachine.Context.playerCounter);
                stateMachine.SwitchState(potBetState);
               
            }
            else
            {
                //stateMachine.Context.GameMenu.ShowBetMenuUI(!bot);
            }
         
        }
        else
        {
            //0ar player = stateMachine.Context.GetCurrentPlayer();
            //stateMachine.Context.GameMenu.ShowBetMenuUI(player.IsLocalNetworkPlayer());
        }
    }
    private void OnTimerFinishBet()
    {
        if (AppData.gameType == GameType.AI)
        {
           
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                
            }
        }

      
    }
}