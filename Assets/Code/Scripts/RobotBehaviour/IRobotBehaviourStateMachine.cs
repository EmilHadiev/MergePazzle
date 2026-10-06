public interface IRobotBehaviourStateMachine
{
    void SwitchTo<T>() where T : IRobotBehaviourState;
    void SwitchToPreviouesState();
}