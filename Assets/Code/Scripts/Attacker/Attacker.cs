using System;
using UnityEngine;

public class Attacker : MonoBehaviour, IAttackable
{
    private IAttackLogic _attackLogic;

    public event Action AttackStarting;
    public event Action AttackEnding;

    private void Start()
    {
        var data = GetComponent<IRobot>().Data;
        _attackLogic = GetAttackLogic(data);
    }

    public void Attack()
    {
        _attackLogic.Attack();

        AttackStarting?.Invoke();
    }

    private IAttackLogic GetAttackLogic(RobotData data)
    {
        switch (data.AttackType)
        {
            case AttackType.Melee:
                return new MeleeAttackLogic(data, transform, data.AttackTarget);
            case AttackType.Range:
                IBulletStorage bulletSpanwer = GetComponent<BulletStorage>();
                return new RangeAttackLogic(bulletSpanwer);
            default:
                return null;
        }
    }

    private void AttackStarted()
    {
        Attack();
    }

    private void AttackEnded()
    {
        AttackEnding?.Invoke();
    }
}