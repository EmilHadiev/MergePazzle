using UnityEngine;

public class RobotMoveLogic : IMovable
{
    private readonly Transform _robot;
    private readonly RobotData _data;
    private bool _isWorking;

    public RobotMoveLogic(Transform robot, RobotData speed)
    {
        _robot = robot;
        _data = speed;
    }

    public void StartMove()
    {
        _isWorking = true;
    }

    public void StopMove()
    {
        _isWorking = false;
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        _robot.Translate(_robot.forward * GetSpeed()  * Time.deltaTime, Space.World);
    }

    private float GetSpeed() => _data.MoveSpeed;
}