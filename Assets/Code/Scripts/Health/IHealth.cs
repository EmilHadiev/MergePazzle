using System;

public interface IHealth
{
    event Action<float> DamageTaked;
    event Action Died;
    event Action<float> HealthChanged;

    void TakeDamage(float damage);
}