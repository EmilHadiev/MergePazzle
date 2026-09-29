public class RobotAttackState : IRobotBehaviourState
{
    private readonly IAttackPerformer _attacker;

    public RobotAttackState(IAttackPerformer attackLogic)
    {
        _attacker = attackLogic;
    }

    public void Enter()
    {
        _attacker.StartAttacking();
    }

    public void Exit()
    {
        _attacker.StopAttacking();
    }
}