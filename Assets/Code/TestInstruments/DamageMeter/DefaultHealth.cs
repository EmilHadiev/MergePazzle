using System;
using UnityEngine;

public class DefaultHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _healthPoints = 100;

    private float _maxHealth;

    public event Action<float> DamageTaked;
    public event Action Died;
    public event Action<float, float> HealthChanged;

    private void Start()
    {
        _maxHealth = _healthPoints;
    }

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;
        HealthChanged?.Invoke(_healthPoints, _maxHealth);
        DamageTaked?.Invoke(damage);
        Debug.Log($"{_healthPoints} {damage}");
        Die();
    }

    private void Die()
    {
        if (_healthPoints <= 0)
        {
            Died?.Invoke();
            gameObject.SetActive(false);
        }    
    }
}