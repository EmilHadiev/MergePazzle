using System;
using System.Collections.Generic;

public class RobotBehaviourStateMachine : IRobotBehaviourStateMachine
{
    private readonly Dictionary<Type, IRobotBehaviourState> _states;

    private IRobotBehaviourState _currentState;
    private IRobotBehaviourState _previousState;

    public RobotBehaviourStateMachine(IAttackPerformer attackable, IMover mover)
    {
        _states = new Dictionary<Type, IRobotBehaviourState>();
        _states.Add(typeof(RobotAttackState), new RobotAttackState(attackable));
        _states.Add(typeof(RobotMoveState), new RobotMoveState(mover));
        _states.Add(typeof(StubState), new StubState());
    }

    public void SwitchTo<T>() where T : IRobotBehaviourState
    {
        if (_states.TryGetValue(typeof(T), out var value))
        {
            _currentState?.Exit();
            _previousState = _currentState;

            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }
        else
        {
            throw new ArgumentException(nameof(T));
        }
    }

    public void SwitchToPreviouesState()
    {
        _currentState?.Exit();
        _previousState?.Enter();
    }
}