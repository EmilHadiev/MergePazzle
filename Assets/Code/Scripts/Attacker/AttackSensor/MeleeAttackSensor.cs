using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(MeleeTargetObserver))]
public class MeleeAttackSensor : AttackSensor
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private MeleeTargetObserver _targetObserver;

    public override event Action<Collider> TargetDetected;
    public override event Action<Collider> TargetLost;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _targetObserver ??= GetComponent<MeleeTargetObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnTargetDetected;
        _observer.Exited += OnTargetLost;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnTargetDetected;
        _observer.Exited -= OnTargetLost;
    }

    private void Start()
    {
        var robot = GetComponentInParent<IRobot>();
        SetZoneOptions(_observer.GetComponent<SphereCollider>(), robot.Data.AttackRadius);
        _targetObserver.Init(OnTargetLost);
    }

    protected override void OnTargetDetected(Collider target)
    {
        TargetDetected?.Invoke(target);
    }

    protected override void OnTargetLost(Collider target)
    {
        TargetLost?.Invoke(target);
        Debug.Log("LOST!!!");
    }

    private void SetZoneOptions(SphereCollider collider, float attackRadius)
    {
        collider.radius = attackRadius;
        collider.isTrigger = true;
    }
}