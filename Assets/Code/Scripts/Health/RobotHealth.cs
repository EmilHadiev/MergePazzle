using System;
using UnityEngine;

[RequireComponent(typeof(DeathObserver))]
[RequireComponent(typeof(DamageTrigger))]
public class RobotHealth : MonoBehaviour, IHealth
{
    private float _health;
    private float _maxHealth;
    private IRobotDeleter _deleter;
    private IRobotData _data;
    private IRobotUpgrader _upgrader;

    private bool IsDead => _health <= 0;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageTaked;
    public event Action Died;

    private void Awake()
    {
        _deleter = GetComponent<IRobotDeleter>();
        _upgrader = GetComponent<IRobotUpgrader>();
    }

    private void Start()
    {
        _data = GetComponent<IRobot>().Data;
        SetHealth();
    }

    private void OnEnable()
    {
        _deleter.Deleting += OnKill;
        _upgrader.Upgraded += SetHealth;
    }

    private void OnDisable()
    {
        _deleter.Deleting -= OnKill;
        _upgrader.Upgraded -= SetHealth;
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

    private void OnKill()
    {
        Died?.Invoke();
    }

    private void SetHealth()
    {
        _health = _data.GetTotalHealth();
        _maxHealth = _health;
        HealthChanged?.Invoke(_health, _maxHealth);
    }
}