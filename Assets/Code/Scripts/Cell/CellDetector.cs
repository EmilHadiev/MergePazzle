using UnityEngine;
using Zenject;
public class CellDetector : IInitializable, ITickable, ICellDetector
{
    private LayerMask _cellLayer;
    private Camera _camera;
    private Cell _currentCell;

    private bool _isWorking;

    public void StartWorking() => _isWorking = true;

    public void StopWorking()
    {
        if (_currentCell != null)
            _currentCell.Out();

        _currentCell = null;
        _isWorking = false;
    }

    public void Initialize()
    {
        _cellLayer = LayerMask.GetMask(LayerNames.Cell);
        _camera = Camera.main;

        if (_camera == null)
            Debug.LogError($"{_camera}: Camera.main not found");

        StartWorking();
    }

    public void Tick()
    {
        if (_isWorking == false)
            return;

        Cell cell = FindCellUnderCursor();

        if (cell == _currentCell)
            return;

        if (_currentCell != null)
            _currentCell.Out();

        _currentCell = cell;

        if (_currentCell != null)
            _currentCell.Select();
    }

    private Cell FindCellUnderCursor()
    {
        Vector3 screenPoint = Input.mousePosition;

        if (IsInsideScreen(screenPoint) == false)
            return null;

        Ray ray = _camera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _cellLayer) == false)
            return null;

        return hit.collider.TryGetComponent(out Cell cell) ? cell : null;
    }

    private bool IsInsideScreen(Vector3 screenPoint)
    {
        if (float.IsNaN(screenPoint.x) || float.IsNaN(screenPoint.y))
            return false;

        if (float.IsInfinity(screenPoint.x) || float.IsInfinity(screenPoint.y))
            return false;

        return screenPoint.x >= 0f && screenPoint.x <= Screen.width &&
               screenPoint.y >= 0f && screenPoint.y <= Screen.height;
    }
}
