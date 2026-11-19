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

    private Robot _robot;
    private bool _isCellOccupied;
    private bool _isLocked;

    private void OnValidate()
    {
        _colorChanger ??= GetComponent<MaterialColorChanger>();
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

    public void SetView(Robot robot)
    {
        if (_isCellOccupied)
        {
            if (_robot == robot)
            {
                _colorChanger.ChangeColor(_freeColor);
            }
            else
            {
                _colorChanger.ChangeColor(_occupiedColor);
            }           
        }
        else
        {
            _colorChanger.ChangeColor(_freeColor);
        }
    }

    public void TrySet(Robot robot)
    {
        if (_isCellOccupied)
        {
            if (_robot == robot)
            {
                Upgrade(robot, _robot.Upgrader);
                return;
            }

            if (_robot != robot)
            {

            }
        }
        else
        {

        }

        _robot = robot;
    }

    private void SetRobot(Robot robot)
    {
        robot.transform.position = transform.position;
    }

    private void Upgrade(Robot newRobot, IRobotUpgrader currentRobot)
    {
        Destroy(newRobot);
        currentRobot.Upgrade();
    }

    private void ChangeColor(Color color)
    {
        _colorChanger.ChangeColor(color);
    }
}