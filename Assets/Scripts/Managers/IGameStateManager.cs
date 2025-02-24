public interface IGameStateManager
{
    GameState CurrentState { get; }
    void ChangeState(GameState newState);
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    Dialogue,
    Inventory,
    LevelUp,
    GameOver
}