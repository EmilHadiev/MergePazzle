using UnityEngine;

public class RobotMover : MonoBehaviour
{
    private IMovable _movable;

    private void Start()
    {
        Debug.Log("Доработать!");
        var data = GetComponent<IRobot>().Data;
        _movable = new RobotMoveLogic(transform, data);
        _movable.StartMove();
    }

    private void Update()
    {
        _movable.Update();
    }
}