using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameState CurrentGameState;

    #region States

    public GameplayState GameplayState;
    public PauseState PauseState;

    public InventoryState InventoryState;

    #endregion

    private void Awake()
    {
        GameplayState = new GameplayState(this);
        PauseState = new PauseState(this);
        InventoryState = new InventoryState(this);

        CurrentGameState = GameplayState;
        CurrentGameState.EnterState();
    }

    public GameState GetCurrentGameState()
    {
        return CurrentGameState;
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