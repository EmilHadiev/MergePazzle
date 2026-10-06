using UnityEngine;
using Zenject;

public class AttackTargetDetector : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    private IRobotBehaviourStateMachine _stateMachine;

    private void Start()
    {
        var robot = GetComponent<IRobot>();
        _stateMachine = robot.StateMachine;
        SetZoneOptions(_observer.GetComponent<SphereCollider>(), robot.Data.AttackRadius);
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
        _stateMachine.SwitchToPreviouesState();
    }

    private void SetZoneOptions(SphereCollider collider, float attackRadius)
    {
        collider.radius = attackRadius;
        collider.isTrigger = true;
    }
}