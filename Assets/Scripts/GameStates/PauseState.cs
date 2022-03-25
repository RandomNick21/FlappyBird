public class PauseState : BaseState
{
    private bool IsPaused => PauseManager.Instance.IsPaused;

    public override void Start()
    {
        SetOpposite();
    }

    public override void Stop()
    {
        SetOpposite();
    }

    private void SetOpposite()
    {
        PauseManager.Instance.SetPaused(!IsPaused);
    }
}
