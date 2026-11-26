public class IdleState : IRobotBehaviourState
{
    private readonly RobotDeleterAndUpgraderView _viewer;
    private readonly AttackSensor _attackSensor;

    public IdleState(RobotDeleterAndUpgraderView viewer, AttackSensor attackSensor)
    {
        _viewer = viewer;
        _attackSensor = attackSensor;
    }

    public void Enter()
    {
        _viewer.Show();
        _attackSensor.enabled = false;
    }

    public void Exit()
    {
        _viewer.Hide();
        _attackSensor.enabled = true;
    }
}