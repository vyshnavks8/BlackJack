using UnityEngine;

public class RevealState : BlackJackState
{
    [SerializeField] private CheckWinState checkWinState;
    private bool cardsReveled;

    public override void AddListener()
    {
        if (stateMachine.Context.Dealer == null) return;
        stateMachine.Context.Dealer.OnScoreChanged += OnScoreChanged;
    }

    public override void RemoveListener()
    {
        if (stateMachine.Context.Dealer == null) return;
        stateMachine.Context.Dealer.OnScoreChanged -= OnScoreChanged;
    }

    public override void EnterState()
    {
        AddListener();
        stateMachine.Context.RevealDealerCard();
    }


    public override void UpdateState()
    {
        if (!cardsReveled) return;
        stateMachine.SwitchState(checkWinState);
    }

    public override void ExitState()
    {
        cardsReveled = false;
        RemoveListener();
    }

    private void OnScoreChanged(int score)
    {
        cardsReveled = true;
        UpdateState();
    }
}