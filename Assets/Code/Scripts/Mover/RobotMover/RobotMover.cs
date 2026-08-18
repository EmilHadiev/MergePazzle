using UnityEngine;

public class RobotMover : MonoBehaviour
{
    [SerializeField] private RobotData _data;

    private IMovable _movable;

    private void Start()
    {
        Debug.Log("Доработать!");
        _movable = new RobotMoveLogic(transform, _data);
        _movable.StartMove();
    }

    private void Update()
    {
        _movable.Update();
    }
}