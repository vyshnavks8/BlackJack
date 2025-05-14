using UnityEngine;

public class BetOrPassState : BlackJackState
{
    [SerializeField] private BetState  betState;
    private PlayerChoice selectedPlayerChoice = PlayerChoice.None;
    private int passCounter = 0;

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
                UpdateState();
                break;
            case PlayerChoice.Pass:
                selectedPlayerChoice = PlayerChoice.None;
                stateMachine.Context.IncrementNextPlayer();
                passCounter += 1;
                if (passCounter == stateMachine.Context.currentPlayers.Count - 1)
                {
                    // stateMachine.SwitchState(); // restart game
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
        passCounter = 0;
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
        var bot = stateMachine.Context.CheckBotPlaySelect(OnPlayerChoice);
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.GameMenu.ShowPlaySelectMenuUI(!bot);
        }
        else
        {
            var player = stateMachine.Context.GetCurrentPlayer();
            stateMachine.Context.GameMenu.ShowPlaySelectMenuUI(player.IsLocalNetworkPlayer());
        }
    }

    private void StopPlay()
    {
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowPlaySelectMenuUI(false);
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
}