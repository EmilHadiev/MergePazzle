using System;
using UnityEngine;

public class DefaultHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _healthPoints = 100;

    public event Action<float> DamageTaked;
    public event Action Died;
    public event Action<float> HealthChanged;

    public void TakeDamage(float damage)
    {
        _healthPoints -= damage;
        Debug.Log($"{_healthPoints} {damage}");
        Die();
    }

    private void Die()
    {
        if (_healthPoints <= 0)
            gameObject.SetActive(false);
    }
}