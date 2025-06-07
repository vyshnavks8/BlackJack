using UnityEngine;

public abstract class BlackJackState : MonoBehaviour
{
    protected BlackJackStateMachine stateMachine;
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    public void Init(BlackJackStateMachine blackJackStateMachine)
    {
       stateMachine = blackJackStateMachine;
    }
    public virtual void AddListener()
    {
        
    }
    public virtual void RemoveListener()
    {
        
    }

    public virtual void OnStateChange(int id)
    {
        
    }
}