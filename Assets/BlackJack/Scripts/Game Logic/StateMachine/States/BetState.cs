public class BetState : BlackJackState
{
    private const int defaultAmount = 1;

    public override void AddListener()
    {
        stateMachine.Context.GameMenu.OnBet += OnBet;
    }

    public override void RemoveListener()
    {
        stateMachine.Context.GameMenu.OnBet -= OnBet;
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
        stateMachine.Context.GameMenu.ShowBetMenuUI(!bot);
    }

    private void StopBet()
    {
        stateMachine.Context.StopPlayerTimer();
        stateMachine.Context.GameMenu.ShowBetMenuUI(false);
    }

    private void OnTimerFinishBet()
    {
        OnBet(defaultAmount);
    }
}