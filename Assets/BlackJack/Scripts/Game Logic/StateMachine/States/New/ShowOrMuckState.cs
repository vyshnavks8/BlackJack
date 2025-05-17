using UnityEngine;

public class ShowOrMuckState : BlackJackState
{
    [SerializeField] private DiscardState discardState;
    private PlayerChoice selectedPlayerChoice = PlayerChoice.None;
    private BlackJackPlayer player;
    private BlackJackPlayer dealer;

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
        AddListener();
        dealer = stateMachine.Context.Dealer;
        player = stateMachine.Context.GetCurrentPlayer();
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
            case PlayerChoice.Show:
                if (player.Score > dealer.Score)
                {
                    stateMachine.Context.MakePlayerWinner(dealer, player, ChangeState);
                }
                else 
                {
                    stateMachine.Context.MakeDealerWinner(dealer, player, ChangeState);
                }

                break;
            case PlayerChoice.Muck:
                stateMachine.Context.MakeDealerWinner(dealer, player, ChangeState);
                break;
        }
    }

    public override void UpdateState()
    {
        if (selectedPlayerChoice == PlayerChoice.None)
        {
            StartUpdate();
        }
    }


    public override void ExitState()
    {
        
        StopPlay();
        RemoveListener();
    }


    private void ChangeState()
    {
        stateMachine.SwitchState(discardState);
    }

    private void StartUpdate()
    {
        stateMachine.Context.StartPlayerTimer(OnTimerFinishPlay);
        var bot = stateMachine.Context.CheckBotShowOrMuck(OnPlayerChoice);
        if (AppData.gameType == GameType.AI)
        {
            stateMachine.Context.GameMenu.ShowMuckMenuUI(!bot);
        }
        else
        {
            stateMachine.Context.GameMenu.ShowMuckMenuUI(player.IsLocalNetworkPlayer());
        }
    }

    private void StopPlay()
    {
        selectedPlayerChoice= PlayerChoice.None;
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowMuckMenuUI(false);
    }

    private void OnTimerFinishPlay()
    {
        if (AppData.gameType == GameType.AI)
        {
            OnPlayerChoice(PlayerChoice.Muck);
        }
        else
        {
            if (player.IsLocalNetworkPlayer())
            {
                OnPlayerChoice(PlayerChoice.Muck);
            }
        }
    }
}