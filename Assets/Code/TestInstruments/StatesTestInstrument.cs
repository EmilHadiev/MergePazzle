using UnityEngine;

public class StatesTestInstrument : MonoBehaviour
{
    [SerializeField] private Robot[] _robots;

    private IRobotBehaviourStateMachine[] _states;

    private void Start()
    {
        _states = new IRobotBehaviourStateMachine[_robots.Length];

        for (int i = 0; i < _robots.Length; i++)
            _states[i] = _robots[i].StateMachine;

        Debug.Log($"1 - stub");
        Debug.Log($"2 - move");
        Debug.Log($"3 - attack");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            StubState();

        if (Input.GetKeyUp(KeyCode.Alpha2))
            MoveState();

        if (Input.GetKeyUp(KeyCode.Alpha3))
            AttackState();
    }

    private void StubState()
    {
        foreach (var state in _states)
            state.SwitchTo<StubState>();
    }

    private void MoveState()
    {
        foreach (var state in _states)
            state.SwitchTo<RobotMoveState>();
    }

    public void AttackState()
    {
        foreach (var state in _states)
            state.SwitchTo<RobotAttackState>();
    }
}
