public interface IRobot
{
    ICharacterAnimator Animator { get; }
    IRobotBehaviourStateMachine StateMachine { get; }
    IRobotData Data { get; }
}