using UnityEngine;

public class RobotMoveLogic : IMovable
{
    private readonly Transform _robot;
    private readonly RobotData _data;
    private readonly ICharacterAnimator _animator;
    private bool _isWorking;

    public RobotMoveLogic(Transform robot, RobotData speed, ICharacterAnimator animator)
    {
        _robot = robot;
        _data = speed;
        _animator = animator;
    }

    public void StartMove()
    {
        _isWorking = true;
        _animator.StartRunning();
    }

    public void StopMove()
    {
        _isWorking = false;
        _animator.StopRunning();
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        _robot.Translate(_robot.forward * GetSpeed()  * Time.deltaTime, Space.World);
    }

    private float GetSpeed() => _data.MoveSpeed;
}