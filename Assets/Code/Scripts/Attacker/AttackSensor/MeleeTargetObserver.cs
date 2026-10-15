using System;
using UnityEngine;

public class MeleeTargetObserver : MonoBehaviour
{
    [SerializeField] private MeleeAttackSensor _attackSensor;

    private IHealth _currentTarget;
    private Collider _target;
    private Action<Collider> _targetLost;

    private void OnValidate()
    {
        _attackSensor ??= GetComponent<MeleeAttackSensor>();
    }

    private void OnEnable()
    {
        _attackSensor.TargetDetected += OnTargetDetected;
        _attackSensor.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _attackSensor.TargetDetected -= OnTargetDetected;
        _attackSensor.TargetLost -= OnTargetLost;
    }

    public void Init(Action<Collider> targetLost)
    {
        _targetLost = targetLost;
    }

    private void OnTargetDetected(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            _currentTarget = health;
            _currentTarget.Died += OnDied;
        }

        _target = collider;
    }

    private void OnTargetLost(Collider collider)
    {
        RemoveTarget();
    }

    private void RemoveTarget()
    {
        if (_currentTarget != null)
        {
            _currentTarget.Died -= OnDied;
        }
    }

    private void OnDied()
    {
        _targetLost?.Invoke(_target);
    }
}