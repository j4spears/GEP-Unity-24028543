using UnityEngine;

public class GameplayState : GameState
{
    public GameplayState(GameManager GameManager):base(GameManager){}

    public override void EnterState()
    {
        Time.timeScale = 1.0f;
    }

    public override void FrameUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.ChangeState(GameManager.PauseState);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            GameManager.ChangeState(GameManager.InventoryState);
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
