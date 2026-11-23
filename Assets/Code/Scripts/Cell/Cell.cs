using System;
using UnityEngine;

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
            _robotDeleter.Deleting += OnDeleting;
        }
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
        robot.SetPositionAndRotation(transform.position, default);
    }
}