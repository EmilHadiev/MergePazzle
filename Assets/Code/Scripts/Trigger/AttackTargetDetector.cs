using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    private static readonly Predicate<Collider> IsDestroyed = collider => collider == null;

    [SerializeField] private AttackSensor _sensor;

    private readonly List<Collider> _targets = new();

    private IRobotBehaviourStateMachine _stateMachine;
    private IRobot _robot;

    private void OnValidate()
    {
        _sensor ??= GetComponentInChildren<AttackSensor>();
    }

    private void Start()
    {
        _robot = GetComponent<IRobot>();
        _stateMachine = _robot.StateMachine;
    }

    private void OnEnable()
    {
        _sensor.TargetDetected += TargetFounded;
        _sensor.TargetLost += TargetLost;
    }

    private void OnDisable()
    {
        _sensor.TargetDetected -= TargetFounded;
        _sensor.TargetLost -= TargetLost;
        _targets.Clear();
    }

    private void TargetFounded(Collider collider)
    {
        if (IsEnemy(collider) == false || _targets.Contains(collider))
            return;

        _targets.RemoveAll(IsDestroyed);

        bool isFirstTarget = _targets.Count == 0;
        _targets.Add(collider);

        if (isFirstTarget)
            _stateMachine.SwitchTo<RobotAttackState>();
    }

    private void TargetLost(Collider collider)
    {
        if (_targets.Remove(collider) == false)
            return;

        _targets.RemoveAll(IsDestroyed);

        if (_targets.Count == 0)
            _stateMachine.SwitchTo<StubState>();
    }

    private bool IsEnemy(Collider collider)
    {
        IRobot otherRobot = collider.GetComponentInParent<IRobot>();

        if (otherRobot == null)
            return true;

        if (_robot != null && otherRobot == _robot)
            return false;

        return otherRobot.Side != _robot.Side;
    }
}
