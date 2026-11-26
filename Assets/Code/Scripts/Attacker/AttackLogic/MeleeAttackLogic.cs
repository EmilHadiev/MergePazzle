using UnityEngine;

public class MeleeAttackLogic : IAttackLogic
{
    private readonly int _attackCount;
    private readonly Transform _meleeAttackSensor;
    private readonly IRobot _owner;
    private readonly IRobotData _robotData;
    private readonly Collider[] _targets;
    private readonly LayerMask _layerTarget;

    public MeleeAttackLogic(IRobotData robotData, Transform robot, LayerMask layerTarget, int attackCount = 1)
    {
        _layerTarget = layerTarget;
        _meleeAttackSensor = robot;
        _owner = robot.GetComponentInParent<IRobot>();
        _robotData = robotData;
        _attackCount = attackCount;
        _targets = new Collider[_attackCount];
    }

    public void Attack()
    {
        TryToAttack();
    }

    private void TryToAttack()
    {
        int targetCount = Physics.OverlapSphereNonAlloc(_meleeAttackSensor.position, _robotData.AttackRadius, _targets, _layerTarget);
        PhysicsDebug.DrawDebug(_meleeAttackSensor.position, _robotData.AttackRadius);

        if (targetCount == 0)
            return;

        for (int i = 0; i < targetCount; i++)
        {
            if (_owner != null && _targets[i].GetComponentInParent<IRobot>() == _owner)
                continue;

            if (_targets[i].TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_robotData.GetTotalDamage());
            }
        }
    }
}