using System;
using UnityEngine;

public class DealerCardPlaceState : BlackJackState
{
    [SerializeField] private BetOrPassState betOrPassState;
    public override void EnterState()
    {
        
    }

    private void OnCompletePlaceDealerCard(bool obj)
    {
        stateMachine.SwitchState(betOrPassState);
    }

    public override void UpdateState()
    {
        stateMachine.Context.PlaceDealerCard(true, OnCompletePlaceDealerCard);
    }

    public override void ExitState()
    {
       
    }
}