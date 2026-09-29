public class RobotMoveState : IRobotBehaviourState
{
    private readonly IMover _mover;

    public RobotMoveState(IMover mover)
    {
        _mover = mover;
    }

    public void Enter()
    {
        _mover.StartMoving();
    }

    public void Exit()
    {
        _mover.StopMoving();
    }
}