using System.Collections.Generic;
using UnityEngine;

public class GlobalGameStateMachine : MonoBehaviour
{
    [SerializeField] private GameOverStateView GameOverStateView;

    private BaseState CurrentState { get; set; }
    public static GlobalGameStateMachine Instance { get; private set; }

    private readonly List<StateFacade> _stateFacades = new List<StateFacade>();

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
        CurrentState?.Update();
    }   

    public void SetState<T>(bool stopLast = true) where T : BaseState
    {
        var state = _stateFacades.Find(x => x.Type == typeof(T)).State;
        
        if(state == CurrentState)
            return;
        
        if(stopLast) CurrentState?.Stop();
        CurrentState = state;
        CurrentState.Start();
    }
    
    public void SetState(GameState gameState, bool stopLast = true)
    {
        var state = _stateFacades.Find(x => x.GameState == gameState).State;
        
        if(state == CurrentState)
            return;
        
        if(stopLast) CurrentState?.Stop();
        CurrentState = state;
        CurrentState.Start();
    }
}