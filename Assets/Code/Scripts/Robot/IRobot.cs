using UnityEngine;

public interface IRobot
{
    ICharacterAnimator Animator { get; }
    IRobotBehaviourStateMachine StateMachine { get; }
    IRobotData Data { get; }
    IRobotUpgrader Upgrader { get; }
    IRobotDeleter Deleter { get; }
}