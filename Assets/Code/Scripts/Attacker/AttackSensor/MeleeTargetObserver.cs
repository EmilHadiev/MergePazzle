using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTargetObserver : MonoBehaviour
{
    [SerializeField] private MeleeAttackSensor _attackSensor;

    private readonly Dictionary<Collider, Target> _targets = new();

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
        Clear();
    }

    public void Init(Action<Collider> targetLost)
    {
        _targetLost = targetLost;
    }

    private void OnTargetDetected(Collider collider)
    {
        if (_targets.ContainsKey(collider))
            return;

        if (collider.TryGetComponent(out IHealth health) == false)
            return;

        Action diedHandler = () => OnTargetDied(collider);

        health.Died += diedHandler;
        _targets.Add(collider, new Target(health, diedHandler));
    }

    private void OnTargetLost(Collider collider)
    {
        Remove(collider);
    }

    private void OnTargetDied(Collider collider)
    {
        if (Remove(collider) == false)
            return;

        _targetLost?.Invoke(collider);
    }

    private bool Remove(Collider collider)
    {
        if (_targets.TryGetValue(collider, out Target target) == false)
            return false;

        _targets.Remove(collider);
        Unsubscribe(target);

        return true;
    }

    private void Clear()
    {
        foreach (Target target in _targets.Values)
            Unsubscribe(target);

        _targets.Clear();
    }

    private static void Unsubscribe(Target target)
    {
        if (target.Health is UnityEngine.Object unityObject && unityObject != null)
            target.Health.Died -= target.DiedHandler;
    }

    private sealed class Target
    {
        public readonly IHealth Health;
        public readonly Action DiedHandler;

        public Target(IHealth health, Action diedHandler)
        {
            Health = health;
            DiedHandler = diedHandler;
        }
    }
}
