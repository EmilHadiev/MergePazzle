using System;
using UnityEngine;

[RequireComponent(typeof(DeathObserver))]
public class RobotHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health = 10;

    private ICharacterAnimator _animator;
    private float _maxHealth;

    public event Action<float, float> HealthChanged;
    public event Action<float> DamageTaked;
    public event Action Died;

    private void Awake()
    {
        _animator = GetComponent<CharacterAnimator>();
        _maxHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        _health -= damage;

        _animator.TakeDamageTrigger();
        CheckDeadStatus();

        HealthChanged?.Invoke(_health, _maxHealth);
        DamageTaked?.Invoke(damage);
    }

    private void CheckDeadStatus()
    {
        if (_health <= 0)
            Died?.Invoke();
    }
}