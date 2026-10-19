using System;

public interface IHealth
{
    event Action<float> DamageTaked;
    event Action Died;
    /// <summary>
    /// first param - current health
    /// second param - max health
    /// </summary>
    event Action<float, float> HealthChanged;

    void TakeDamage(float damage);
}