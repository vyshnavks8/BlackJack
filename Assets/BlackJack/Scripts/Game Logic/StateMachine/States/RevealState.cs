using UnityEngine;

public class RevealState : BlackJackState
{
    private bool cardsReveled;

    public override void AddListener()
    {
        stateMachine.Context.Dealer.OnScoreChanged += OnScoreChanged;
    }

    public override void RemoveListener()
    {
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
        stateMachine.SwitchState();
    }

    public override void ExitState()
    {
        RemoveListener();
    }

    private void OnScoreChanged(int score)
    {
        cardsReveled = true;
        if (score <= 16)
        {
            stateMachine.Context.PlaceDealerCard(true);
        }
        else
        {
            UpdateState();
        }
    }
}