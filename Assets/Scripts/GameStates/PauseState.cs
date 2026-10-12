using UnityEngine;

public class PauseState : GameState
{
    public PauseState(GameManager GameManager):base(GameManager){}

    public override void EnterState()
    {
        Time.timeScale = 0.0f;
    }

    public override void FrameUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.ChangeState(GameManager.GameplayState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
