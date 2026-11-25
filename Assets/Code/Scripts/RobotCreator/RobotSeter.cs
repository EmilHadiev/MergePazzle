using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RobotSeter : MonoBehaviour
{
    [SerializeField] private RobotsName _robotName;

    [Inject] private IRobotFactory _factory;
    [Inject] private ICellDetector _detector;
    [Inject] private IRobotContainer _container;

    private void OnEnable()
    {
        _detector.CellSelected += OnCellSelected;
    }

    private void OnDisable()
    {
        _detector.CellSelected -= OnCellSelected;
    }

    private void OnCellSelected(Cell cell)
    {
        TryCreateRobot(cell).Forget();
    }

    private async UniTask TryCreateRobot(Cell cell)
    {
        if (cell.IsFree(_robotName))
        {
            var robot = await _factory.CreateRobot(_robotName);
            cell.TrySet(robot.transform, robot.Deleter);            
            _container.Add(robot);
        }
    }
}
