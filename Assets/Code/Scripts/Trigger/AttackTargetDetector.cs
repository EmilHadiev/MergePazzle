using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField] private AttackSensor _sensor;

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
    }

    private void TargetFounded(Collider collider)
    {
        if (collider.TryGetComponent(out IRobot otherRobot))
        {
            if (otherRobot.Side == _robot.Side)
                return;
        }

        _stateMachine.SwitchTo<RobotAttackState>();
    }

    private void TargetLost(Collider collider)
    {
        _stateMachine.SwitchTo<IdleState>();
    }
}