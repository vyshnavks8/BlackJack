using UnityEngine;

public class PlayState : BlackJackState
{
    [SerializeField] private RevealState revealState;
    [SerializeField] private CheckWinState checkWinState;
    private PlayerChoice selectedPlayerChoice = PlayerChoice.None;
    private int CurrentPlayer = -1;
    private bool dealerPlayed;

    public override void AddListener()
    {
        stateMachine.Context.GameMenu.OnPlayerChoice += OnPlayerChoice;
        stateMachine.NetworkEventSender.OnPlayerChoice += SetPlayerChoice;
    }

    public override void RemoveListener()
    {
        stateMachine.Context.GameMenu.OnPlayerChoice -= OnPlayerChoice;
        stateMachine.NetworkEventSender.OnPlayerChoice -= SetPlayerChoice;
        stateMachine.Context.StopCheckBot();
    }

    public override void EnterState()
    {
        CurrentPlayer = stateMachine.Context.playerCounter;
        AddListener();
    }


    private void OnPlayerChoice(PlayerChoice playerChoice)
    {
        if (AppData.gameType == GameType.AI)
        {
            SetPlayerChoice(playerChoice);
        }
        else
        {
            stateMachine.NetworkEventSender.PlacePlayerChoice(playerChoice);
        }
    }

    private void SetPlayerChoice(PlayerChoice playerChoice)
    {
        StopPlay();
        selectedPlayerChoice = playerChoice;
        switch (selectedPlayerChoice)
        {
            case PlayerChoice.Hit:
                UpdateState();
                break;
            case PlayerChoice.Stand:
                selectedPlayerChoice = PlayerChoice.None;
                stateMachine.Context.playerCounter = stateMachine.Context.DealerIndex;
                if (dealerPlayed)
                {
                    stateMachine.SwitchState(revealState);
                }
                else
                {
                    dealerPlayed = true;
                    UpdateState();
                }

                break;
        }
    }

    public override void UpdateState()
    {
        if (selectedPlayerChoice == PlayerChoice.None)
        {
            StartPlay();
            return;
        }

        PlaceCards();
    }


    private void OnCompleted(bool completed)
    {
        var status = stateMachine.Context.CheckCurrentPlayerStatus();
        if (status is PlayerStatus.Won or PlayerStatus.Busted)
        {
            stateMachine.Context.ShowCurrentPlayerStatus();
            if (dealerPlayed)
            {
                stateMachine.SwitchState(revealState);
                return;
            }

            stateMachine.SwitchState(checkWinState);
            return;
        }

        StartPlay();
    }

    public override void ExitState()
    {
        dealerPlayed = false;
        stateMachine.Context.playerCounter=CurrentPlayer;
        CurrentPlayer = -1;
        StopPlay();
        RemoveListener();
    }


    private void PlaceCards()
    {
        stateMachine.Context.PlacePlayerCard(OnCompleted);
    }

    private void StartPlay()
    {
        stateMachine.Context.StartPlayerTimer(OnTimerFinishPlay);
        var bot = stateMachine.Context.CheckBotPlayChoice(OnPlayerChoice);
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.GameMenu.ShowPlayChoiceMenuUI(!bot);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.GameMenu.ShowPlayChoiceMenuUI(player.IsLocalNetworkPlayer());
        }
    }

    private void StopPlay()
    {
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowPlayChoiceMenuUI(false);
    }

    private void OnTimerFinishPlay()
    {
        if (AppData.gameType == GameType.AI)
        {
            OnPlayerChoice(PlayerChoice.Stand);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                OnPlayerChoice(PlayerChoice.Stand);
            }
        }
    }
}