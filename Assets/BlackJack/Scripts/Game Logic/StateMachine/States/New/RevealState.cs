using UnityEngine;

public class RevealState : BlackJackState
{
    [SerializeField] private CheckWinState checkWinState;
    private bool cardsReveled;

    public override void AddListener()
    {
     
    }

    public override void RemoveListener()
    {
    }

    public override void EnterState()
    {
        AddListener();
        
    }


    public override void UpdateState()
    {
        stateMachine.Context.Dealer.RevealCards(OnCardReveal);
 
    }

    private void OnCardReveal()
    {
        stateMachine.SwitchState(checkWinState);
    }

    public override void ExitState()
    {
        RemoveListener();
    }
    
}