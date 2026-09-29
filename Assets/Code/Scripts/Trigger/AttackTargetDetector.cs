using UnityEngine;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    private IRobotBehaviourStateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = GetComponent<IRobot>().StateMachine;
    }

    private void OnEnable()
    {
        _observer.Entered += TargetFounded;
        _observer.Exited += TargetLost;
    }

    private void OnDisable()
    {
        _observer.Entered -= TargetFounded;
        _observer.Exited -= TargetLost;
    }

    private void TargetFounded(Collider collider)
    {
        _stateMachine.SwitchTo<RobotAttackState>();
    }

    private void TargetLost(Collider collider)
    {
        _stateMachine.SwitchTo<RobotMoveState>();
    }
}