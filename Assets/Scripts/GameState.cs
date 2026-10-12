using UnityEngine;

public class GameState
{
    protected GameManager GameManager;

    public GameState(GameManager GameManager)
    {
        this.GameManager = GameManager;
    }

    public virtual void EnterState(){}
    public virtual void FrameUpdate(){}
    public virtual void PhysicsUpdate(){}
    public virtual void ExitState(){}
}
