using UnityEngine;

public enum GameStates {GAMEPLAY, PAUSE}

public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState;

    #region States

    public GameplayState GameplayState;
    public PauseState PauseState;

    #endregion


    private void Awake()
    {
        GameplayState = new GameplayState(this);
        PauseState = new PauseState(this);

        CurrentGameState = GameplayState;
        CurrentGameState.EnterState();
    }

    public void ChangeState(GameState new_state)
    {
        CurrentGameState.ExitState();
        CurrentGameState = new_state;
        CurrentGameState.EnterState();
    }

    private void Update()
    {
        CurrentGameState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        CurrentGameState.PhysicsUpdate();
    }
}