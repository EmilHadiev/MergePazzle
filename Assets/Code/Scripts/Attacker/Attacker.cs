using UnityEngine;

public class Attacker : MonoBehaviour, IAttackable
{
    private IAttackLogic _attackLogic;

    private void Start()
    {
        var data = GetComponent<IRobot>().Data;
        _attackLogic = GetAttackLogic(data);
    }

    public void Attack()
    {
        _attackLogic.Attack();
    }

    private IAttackLogic GetAttackLogic(RobotData data)
    {
        switch (data.AttackType)
        {
            case AttackType.Melee:
                return new MeleeAttackLogic(data, transform, data.AttackTarget);
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
        Debug.Log(gameObject.name + " attack ended");
    }
}