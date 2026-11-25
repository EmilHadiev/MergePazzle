public class IdleState : IRobotBehaviourState
{
    private readonly RobotDeleterAndUpgraderView _viewer;

    public IdleState(RobotDeleterAndUpgraderView viewer)
    {
        _viewer = viewer;
    }

    public void Enter()
    {
        _viewer.Show();
    }

    public void Exit()
    {
        _viewer.Hide();
    }
}