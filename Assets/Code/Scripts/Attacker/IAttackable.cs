using System;

public interface IAttackable
{
    public event Action AttackStarting;
    public event Action AttackEnding;

    void Attack();
}