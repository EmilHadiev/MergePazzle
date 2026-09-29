public interface IRobot
{
    ICharacterAnimator Animator { get; }
    IRobotBehaviourStateMachine StateMachine { get; }
    RobotData Data { get; }
}