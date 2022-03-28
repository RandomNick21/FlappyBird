using System.Collections.Generic;
using UnityEngine;

public class GlobalGameStateMachine : MonoBehaviour
{
    [SerializeField] private GameOverStateView GameOverStateView;
    private readonly List<StateFacade> _stateFacades = new List<StateFacade>();
    private BaseState _currentState;
    
    public static GlobalGameStateMachine Instance { get; private set; }

    private void Awake()
    {
        _stateFacades
            .Append(new StateFacade(GameState.Pause, typeof(PauseState), new PauseState()))
            .Append(new StateFacade(GameState.GameOver, typeof(GameOverState), new GameOverState(GameOverStateView)))
            .Append(new StateFacade(GameState.Game, typeof(GameplayState), new GameplayState()));
        
        Instance = this;
        SetState<GameplayState>();
    }

    private void Update()
    {
        _currentState?.Update();
    }   

    public void SetState<T>(bool stopLast = true) where T : BaseState
    {
        var state = _stateFacades.Find(x => x.Type == typeof(T)).State;
        SetStateBase(state, stopLast);
    }
    
    public void SetState(GameState gameState, bool stopLast = true)
    {
        var state = _stateFacades.Find(x => x.GameState == gameState).State;
        SetStateBase(state, stopLast);
    }

    private void SetStateBase(BaseState state, bool stopLast)
    {
        if(state == _currentState)
            return;
        
        if(stopLast) _currentState?.Stop();
        _currentState = state;
        _currentState.Start();
    }
}