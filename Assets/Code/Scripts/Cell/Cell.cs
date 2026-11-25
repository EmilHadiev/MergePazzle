using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MaterialColorChanger))]
public class Cell : MonoBehaviour
{
    [SerializeField] private MaterialColorChanger _colorChanger;

    [SerializeField] private Color _upgradeColor;
    [SerializeField] private Color _freeColor;
    [SerializeField] private Color _occupiedColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _lockColor;

    private IRobotDeleter _robotDeleter;
    private bool _isCellOccupied;
    private bool _isLocked;
    private float _yAngle;
    private RobotSide _side;

    private void OnValidate()
    {
        _colorChanger ??= GetComponent<MaterialColorChanger>();
    }

    private void OnDestroy()
    {
        OnDeleting();
    }

    public void Select()
    {
        if (_isLocked)
            return;

        if (_isCellOccupied)
            ChangeColor(_occupiedColor);
        else
            ChangeColor(_freeColor);
    }

    public void Out()
    {
        if (_isLocked)
            return;

        ChangeColor(_defaultColor);
    }

    public void Lock()
    {
        ChangeColor(_lockColor);
        _isLocked = true;
    }

    public void SetRotation(float yAngle)
    {
        _yAngle = yAngle;
    }

    public void SetSide(RobotSide side)
    {
        _side = side;
    }

    public bool IsFree(RobotsName name)
    {
        if (_isLocked)
            return false;

        return _isCellOccupied == false;
    }

    public void TrySet(Transform robot, IRobotDeleter deleter)
    {
        if (_isLocked)
            return;

        if (_isCellOccupied == false)
        {
            _robotDeleter = deleter;
            _isCellOccupied = true;
            SetPositionAndRotation(robot);
            SetRobotSide(robot);
            _robotDeleter.Deleting += OnDeleting;
        }
    }

    private void SetRobotSide(Transform robot)
    {
        if (robot.TryGetComponent(out Robot robotComponent))
            robotComponent.SetSide(_side);
    }

    private void OnDeleting()
    {
        if (_robotDeleter != null)
        {
            _robotDeleter.Deleting -= OnDeleting;
            _robotDeleter = null;
            _isCellOccupied = false;
        }        
    }

    private void ChangeColor(Color color)
    {
        _colorChanger.ChangeColor(color);
    }

    private void SetPositionAndRotation(Transform robot)
    {
        Vector3 rotation = new Vector3(0, _yAngle, 0);
        robot.SetPositionAndRotation(transform.position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
    }
}