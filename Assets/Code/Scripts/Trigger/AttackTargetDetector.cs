using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField] private AttackSensor _sensor;

    private IRobotBehaviourStateMachine _stateMachine;

    private void OnValidate()
    {
        _sensor ??= GetComponentInChildren<AttackSensor>();
    }

    private void Start()
    {
        var robot = GetComponent<IRobot>();
        _stateMachine = robot.StateMachine;        
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
        _stateMachine.SwitchTo<RobotAttackState>();
    }

    private void TargetLost(Collider collider)
    {
        _stateMachine.SwitchTo<StubState>();
    }
}