using System;
using UnityEngine;

[RequireComponent(typeof(DeathObserver))]
[RequireComponent(typeof(DamageTrigger))]
public class RobotHealth : MonoBehaviour, IHealth
{
    private float _health;
    private float _maxHealth;
    private bool IsDead => _health <= 0;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageTaked;
    public event Action Died;

    private void Start()
    {
        var robot = GetComponent<IRobot>();
        _health = robot.Data.StartHealth;
        _maxHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0 || IsDead)
            return;

        _health -= damage;

        CheckDeadStatus();

        HealthChanged?.Invoke(_health, _maxHealth);
        DamageTaked?.Invoke(damage);
    }

    private void CheckDeadStatus()
    {
        if (IsDead)
            Died?.Invoke();
    }
}