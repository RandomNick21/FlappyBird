using System;

public readonly struct StateFacade
{
    public readonly GameState GameState;
    public readonly Type Type;
    public readonly BaseState State;

    public StateFacade(GameState gameState, Type type, BaseState state)
    {
        GameState = gameState;
        Type = type;
        State = state;
    }
}