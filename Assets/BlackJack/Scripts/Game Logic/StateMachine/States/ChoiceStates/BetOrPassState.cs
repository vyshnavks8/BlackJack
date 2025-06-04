using UnityEngine;

public class BetOrPassState : BlackJackState
{
    [SerializeField] private BetState betState;
    [SerializeField] private FinishState finishState;
    private PlayerChoice selectedPlayerChoice = PlayerChoice.None;

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
        stateMachine.Context.IncrementNextPlayer();
        AddListener();
    }

    private void OnPlayerChoice(PlayerChoice playerChoice)
    {
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.ShowInfo(playerChoice.ToString());
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
            case PlayerChoice.Bet:
                stateMachine.Context.IncrementPassCounter();
                UpdateState();
                break;
            case PlayerChoice.Pass:
                selectedPlayerChoice = PlayerChoice.None;
                stateMachine.Context.IncrementNextPlayer();
                stateMachine.Context.IncrementPassCounter();
                if (stateMachine.Context.IsMaxPlayerReached)
                {
                    stateMachine.SwitchState(finishState);
                }
                else
                {
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

        StartBet();
    }


    public override void ExitState()
    {
        selectedPlayerChoice = PlayerChoice.None;
        StopPlay();
        RemoveListener();
    }


    private void StartBet()
    {
        stateMachine.SwitchState(betState);
    }

    private void StartPlay()
    {
        stateMachine.Context.StartPlayerTimer(OnTimerFinishPlay);
        var bot = stateMachine.Context.CheckBotBetOrPass(OnPlayerChoice);
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.GameMenu.ShowBetPassMenuUI(!bot);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.GameMenu.ShowBetPassMenuUI(player.IsLocalNetworkPlayer());
        }
    }

    private void StopPlay()
    {
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowBetPassMenuUI(false);
    }

    private void OnTimerFinishPlay()
    {
        if (AppData.gameType == GameType.AI)
        {
            OnPlayerChoice(PlayerChoice.Pass);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            if (player.IsLocalNetworkPlayer())
            {
                OnPlayerChoice(PlayerChoice.Pass);
            }
        }
    }

    public override void OnStateChange()
    {
        var currentPlayer = stateMachine.Context.GetCurrentPlayer();
        currentPlayer.StopTimer();
        foreach (var removedPlayer in stateMachine.Context.removedPlayers)
        {
            stateMachine.Context.RemoveFromCurrentPlayer(removedPlayer);
        }

        if (!NetworkManager.IsMasterClient) return;
        foreach (var removedPlayer in stateMachine.Context.removedPlayers)
        {
            Debug.Log(removedPlayer.NetworkID + " removedPlayer" + currentPlayer.NetworkID);
            if (removedPlayer.NetworkID == currentPlayer.NetworkID)
            {
                OnPlayerChoice(PlayerChoice.Pass);
                Debug.Log("pass");
                break;
            }
        }
    }
}