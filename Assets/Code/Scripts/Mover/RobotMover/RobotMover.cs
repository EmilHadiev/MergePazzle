using UnityEngine;

public class RobotMover : MonoBehaviour, IMover
{
    private IMovable _movable;

    private void Start()
    {
        Debug.Log("Доработать!");
        IRobot robot = GetComponent<IRobot>();
        var data = robot.Data;
        var animator = robot.Animator;
        _movable = new RobotMoveLogic(transform, data, animator);
        StopMoving();
    }

    private void Update()
    {
        _movable.Update();
    }

    public void StartMoving()
    {
        _movable.StartMove();
    }

    public void StopMoving()
    {
        _movable.StopMove();
    }
}