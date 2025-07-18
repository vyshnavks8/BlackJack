using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PotBetState : BlackJackState
{
    private const int defaultAmount = 10;
    [SerializeField] private PlayersCardPlaceState playersCardPlaceState;
    [SerializeField] private SelectDealerState selectDealerState;

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
        stateMachine.Context.PlacePlayerChip(amount, OnCompletedBet, false, true);
    }

    private void OnCompletedBet(bool obj)
    {
        stateMachine.SwitchState(playersCardPlaceState);
    }

    private void StartBet()
    {
        stateMachine.Context.StartPlayerTimer(OnTimerFinishBet);
        var bot = stateMachine.Context.CheckBotBet(stateMachine.Context.Dealer.BetAmount, OnBet);
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
            TimeOut(() => { OnBet(defaultAmount); });
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                TimeOut(() => { OnBet(defaultAmount); });
            }
        }
    }

    private void TimeOut(Action callback = null)
    {
        stateMachine.Context.ShowInfoForce($"Time Out", callback);
        stateMachine.Context.GameMenu.HideBetOverlay();
    }

    public override void OnStateChange(int id)
    {
        var currentPlayer = stateMachine.Context.Dealer;
        if (currentPlayer.NetworkID == id)
        {
            currentPlayer.StopTimer();
            currentPlayer.blackJackPlayerUI.DisablePlayerUI();
            stateMachine.SwitchState(selectDealerState);
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