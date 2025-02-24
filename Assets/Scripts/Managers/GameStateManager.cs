using UnityEngine;
using Zenject;

public class GameStateManager : IGameStateManager
{
    public GameState CurrentState { get; private set; } = GameState.Playing;

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"Game state changed to: {newState}");
    }
}
