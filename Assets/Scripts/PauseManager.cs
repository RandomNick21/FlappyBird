using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
    
    public bool IsPaused { get; private set; }
    public static PauseManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }
    
    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        for (var i = 0; i < _handlers.Count; i++)
            _handlers[i].SetPaused(isPaused);
    }
}
