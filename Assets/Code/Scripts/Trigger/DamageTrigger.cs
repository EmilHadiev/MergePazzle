using UnityEngine;

[RequireComponent(typeof(DamageImpactView))]
public class DamageTrigger : MonoBehaviour
{
    private ICharacterAnimator _animator;
    private IHealth _health;

    private void Awake()
    {
        _animator = GetComponent<ICharacterAnimator>();
        _health = GetComponent<IHealth>();
    }

    private void OnEnable()
    {
        _health.DamageTaked += OnDamageTaked;
    }

    private void OnDisable()
    {
        _health.DamageTaked -= OnDamageTaked;
    }

    private void OnDamageTaked(float damage)
    {
        _animator.TakeDamageTrigger();
    }
}