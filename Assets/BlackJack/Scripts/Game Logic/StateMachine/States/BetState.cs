using UnityEngine;

public class BetState : BlackJackState
{
    private const int defaultAmount = 1;

    public override void AddListener()
    {
        stateMachine.Context.GameMenu.OnBet += OnBet;
        stateMachine.NetworkEventSender.OnPlaceChipAmount += PlaceChipAmount;
    }


    public override void RemoveListener()
    {
        stateMachine.Context.GameMenu.OnBet -= OnBet;
        stateMachine.NetworkEventSender.OnPlaceChipAmount -= PlaceChipAmount;
        stateMachine.Context.StopCheckBot();
    }

    public override void EnterState()
    {
        stateMachine.Context.playerCounter = 0;
        AddListener();
        StartBet();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        stateMachine.Context.playerCounter = 0;
        RemoveListener();
        stateMachine.Context.GameMenu.ShowBetMenuUI(false);
    }

    private void OnBet(int amount)
    {
        if (AppData.gameType == GameType.AI)
        {
            PlaceChipAmount(amount);
        }
        else
        {
            stateMachine.NetworkEventSender.PlacePlayerChip(amount);
        }
    }

    private void PlaceChipAmount(int amount)
    {
        StopBet();
        stateMachine.Context.PlacePlayerChip(amount, OnCompletedBet);
    }

    private void OnCompletedBet(bool obj)
    {
        stateMachine.Context.playerCounter += 1;
        if (stateMachine.Context.IsMaxPlayerCounter())
        {
            stateMachine.SwitchState();
            return;
        }

        StartBet();
    }

    private void StartBet()
    {
        stateMachine.Context.StartPlayerTimer(OnTimerFinishBet);
        var bot = stateMachine.Context.CheckBotBet(OnBet);
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.GameMenu.ShowBetMenuUI(!bot);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.GameMenu.ShowBetMenuUI(player.IsLocalNetworkPlayer());
        }
    }

    private void StopBet()
    {
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowBetMenuUI(false);
    }

    private void OnTimerFinishBet()
    {
        if (AppData.gameType == GameType.AI)
        {
            OnBet(defaultAmount);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                OnBet(defaultAmount);
            }
        }

      
    }
}