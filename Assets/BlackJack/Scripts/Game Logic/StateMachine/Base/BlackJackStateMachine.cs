using System.Collections.Generic;
using UnityEngine;

public class BlackJackStateMachine : MonoBehaviour
{
    [SerializeField] private BlackJackStateContext context;
    [SerializeField] private GameNetworkEventSender networkEventSender;
    public BlackJackStateContext Context => context;
    public GameNetworkEventSender NetworkEventSender => networkEventSender;
    public List<BlackJackState> state = new();
    private int currentState;

    public void SwitchState()
    {
        if (state[currentState] != null)
        {
            state[currentState].ExitState();
        }

        currentState += 1;
        if (currentState > state.Count)
        {
            return;
        }

        state[currentState].EnterState();
        state[currentState].UpdateState();
    }

    public void Init(BlackJackPlayer dealer, List<BlackJackPlayer> currentPlayers)
    {
        Context.GameMenu.HideUI();
        Context.SetPlayer(dealer,currentPlayers);
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
        currentState = 0;
        state[currentState].EnterState();
        state[currentState].UpdateState();
    }

    public void ResetData()
    {
        foreach (var blackJackState in state)
        {
            blackJackState.RemoveListener();
        }
        currentState = 0;
        context.ResetData();
    }
}