public class GameOverState : BaseState
{
    private readonly WindowActivator _windowActivator;

    public GameOverState(GameOverStateView view)
    {
        _windowActivator = view.GameOverWindow;
    }

    public override void Start()
    {
        _windowActivator.Open();
        PauseManager.Instance.SetPaused(true);
    }

    public override void Stop()
    {
        _windowActivator.Close();
        GlobalGameStateMachine.Instance.SetState<GameplayState>(false);
    }
}
