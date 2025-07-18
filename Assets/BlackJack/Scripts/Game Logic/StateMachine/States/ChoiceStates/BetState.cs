using System;
using UnityEngine;

public class BetState : BlackJackState
{
    [SerializeField] private InitialState initialState;
    [SerializeField] private DiscardState discardState;
    private const int defaultAmount = 1;

    public override void AddListener()
    {
        stateMachine.Context.GameMenu.OnBet += Bet;
        stateMachine.NetworkEventSender.OnPlaceChipAmount += PlaceChipAmount;
    }


    public override void RemoveListener()
    {
        stateMachine.Context.GameMenu.OnBet -= Bet;
        stateMachine.NetworkEventSender.OnPlaceChipAmount -= PlaceChipAmount;
        stateMachine.Context.StopCheckBot();
    }

    public override void EnterState()
    {
        AddListener();
        StartBet();
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        RemoveListener();
        stateMachine.Context.GameMenu.ShowBetMenuUI(false);
    }

    private void Bet(int amount)
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
        stateMachine.Context.ShowInfo($"Bet ${amount}");
        stateMachine.Context.PlacePlayerChip(amount, OnCompletedPlaceChip);
    }

    private void OnCompletedPlaceChip(bool obj)
    {
        stateMachine.SwitchState(initialState);
    }

    private void StartBet()
    {
        stateMachine.Context.StartPlayerTimer(OnTimerFinishBet);
        var bot = stateMachine.Context.CheckBotBet(
            stateMachine.Context.Dealer.BetAmount, Bet);
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

    private void TimeOut(Action callback = null)
    {
        stateMachine.Context.ShowInfoForce($"Time Out", callback);
        stateMachine.Context.GameMenu.HideBetOverlay();
    }

    private void OnTimerFinishBet()
    {
        if (AppData.gameType == GameType.AI)
        {
            TimeOut(() => { Bet(defaultAmount); });
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                TimeOut(() => { Bet(defaultAmount); });
            }
        }
    }

    public override void OnStateChange(int id)
    {
        var currentPlayer = stateMachine.Context.GetCurrentPlayer();
        if (currentPlayer.NetworkID == id)
        {
            currentPlayer.StopTimer();
            currentPlayer.blackJackPlayerUI.DisablePlayerUI();
            stateMachine.SwitchState(discardState);
        }
        else
        {
            foreach (var removedPlayer in stateMachine.Context.removedPlayers)
            {
                removedPlayer.blackJackPlayerUI.DisablePlayerUI();
                stateMachine.Context.RemoveFromCurrentPlayer(removedPlayer);
            }
        }
    }
}