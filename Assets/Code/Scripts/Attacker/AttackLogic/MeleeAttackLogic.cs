using UnityEngine;

public class MeleeAttackLogic : IAttackLogic
{
    private const int AttackTarget = 1;
    private readonly Transform _robot;
    private readonly RobotData _robotData;
    private readonly Collider[] _targets;
    private readonly LayerMask _layerTarget;

    public MeleeAttackLogic(RobotData robotData, Transform robot, LayerMask layerTarget)
    {
        _layerTarget = layerTarget;
        _robot = robot;
        _robotData = robotData;
        _targets = new Collider[AttackTarget];
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

        for (int i = 0; i < _targets.Length; i++)
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