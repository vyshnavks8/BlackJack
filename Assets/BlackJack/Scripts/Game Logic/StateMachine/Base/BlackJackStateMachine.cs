using System;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackStateMachine : MonoBehaviour
{
    [SerializeField] private BlackJackStateContext context;
    [SerializeField] private GameNetworkEventSender networkEventSender;
    public BlackJackStateContext Context => context;
    public GameNetworkEventSender NetworkEventSender => networkEventSender;

    public List<BlackJackState> state = new();

    //private int currentState;
    public BlackJackState startState;
    public BlackJackState currentState;

    private void OnEnable()
    {
        networkEventSender.OnPlayerLeft += OnPlayerLeft;
    }

    private void OnDisable()
    {
        networkEventSender.OnPlayerLeft += OnPlayerLeft;
    }

    private void OnPlayerLeft(int currentPlayer)
    {
        context.AddToRemovedPlayer(currentPlayer);
        currentState.OnStateChange();
    }


    public void SwitchState(BlackJackState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;

        currentState.EnterState();
        currentState.UpdateState();
    }

    public void Init(List<BlackJackPlayer> currentPlayers)
    {
        Context.GameMenu.HideUI();
        Context.SetPlayer(currentPlayers);
        InitStates();
    }

    private void InitStates()
    {
        foreach (var blackJackState in state)
        {
            blackJackState.Init(this);
        }
    }

    public void GotoStartState()
    {
        SwitchState(startState);
    }


    public void ResetData()
    {
        foreach (var blackJackState in state)
        {
            blackJackState.RemoveListener();
        }

        context.ResetData();
    }
}