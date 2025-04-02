public class PlayState : BlackJackState
{
    private PlayerChoice selectedPlayerChoice = PlayerChoice.None;

    public override void AddListener()
    {
        stateMachine.Context.GameMenu.OnPlayerChoice += OnPlayerChoice;
    }

    public override void RemoveListener()
    {
        stateMachine.Context.GameMenu.OnPlayerChoice -= OnPlayerChoice;
        stateMachine.Context.StopCheckBot();
    }

    public override void EnterState()
    {
        stateMachine.Context.playerCounter = 0;
        var wonPlayer = stateMachine.Context.GetWonPlayer();
        foreach (var player in wonPlayer)
        {
            player.ShowStatus();
            stateMachine.Context.AddToRemovedPlayer(player);
            stateMachine.Context.RemoveFromCurrentPlayer(player);
        }

        AddListener();
    }


    private void OnPlayerChoice(PlayerChoice playerChoice)
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
                stateMachine.Context.playerCounter += 1;
                if (stateMachine.Context.IsMaxPlayerCounter())
                {
                    stateMachine.SwitchState();
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

        PlaceCards();
    }


    private void OnCompleted(bool completed)
    {
        var status = stateMachine.Context.CheckCurrentPlayerStatus();
        if (status is PlayerStatus.Won or PlayerStatus.Busted)
        {
            stateMachine.Context.ShowCurrentPlayerStatus();
            stateMachine.Context.playerCounter += 1;
            if (stateMachine.Context.IsMaxPlayerCounter())
            {
                stateMachine.SwitchState();
                return;
            }
        }

        StartPlay();
    }

    public override void ExitState()
    {
        stateMachine.Context.playerCounter = 0;
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
        var bot = stateMachine.Context.CheckBotPlay(OnPlayerChoice);
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.GameMenu.ShowPlayMenuUI(!bot);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.GameMenu.ShowPlayMenuUI(player.IsLocalNetworkPlayer());
        }
    }

    private void StopPlay()
    {
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowPlayMenuUI(false);
    }

    private void OnTimerFinishPlay()
    {
        OnPlayerChoice(PlayerChoice.Stand);
    }
}