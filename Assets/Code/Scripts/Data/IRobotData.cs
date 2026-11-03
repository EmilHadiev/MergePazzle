using UnityEngine;

public interface IRobotData
{
    float AttackRadius { get; }
    float AttackRange { get; }
    LayerMask AttackTarget { get; }
    AttackType AttackType { get; }
    float MoveSpeed { get; }
    RobotsName RobotName { get; }
    float StartAttackSpeed { get; }
    float StartDamage { get; }
    float StartHealth { get; }
    float StartPrice { get; }
}