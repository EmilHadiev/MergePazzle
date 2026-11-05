using UnityEngine;

public class MeleeAttackLogic : IAttackLogic
{
    private readonly int _attackCount;
    private readonly Transform _robot;
    private readonly IRobotData _robotData;
    private readonly Collider[] _targets;
    private readonly LayerMask _layerTarget;

    public MeleeAttackLogic(IRobotData robotData, Transform robot, LayerMask layerTarget, int attackCount = 1)
    {
        _layerTarget = layerTarget;
        _robot = robot;
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
        int targetCount = Physics.OverlapSphereNonAlloc(GetAttackPosition(), _robotData.AttackRadius, _targets, _layerTarget);
        PhysicsDebug.DrawDebug(GetAttackPosition(), _robotData.AttackRadius);

        if (targetCount == 0)
            return;

        for (int i = 0; i < targetCount; i++)
        {
            if (_targets[i].TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_robotData.StartDamage);
            }
        }
    }

    private Vector3 GetAttackPosition()
    {
        return _robot.position + _robot.forward * _robotData.AttackRange;
    }
}