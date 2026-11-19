using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Cell _cellTemplate;
    [SerializeField] private List<bool> _cells;

    [SerializeField, Min(1)] private int _rows = 4;
    [SerializeField, Min(1)] private int _columns = 6;
    [SerializeField, Min(0.01f)] private float _cellSize = 1f;
    [SerializeField, Min(0f)] private float _spacing = 0.1f;

    [Inject] private IFactory _factory;

    private void OnValidate()
    {
        _rows = Mathf.Max(1, _rows);
        _columns = Mathf.Max(1, _columns);
        _cellSize = Mathf.Max(0.01f, _cellSize);
        _spacing = Mathf.Max(0f, _spacing);

        SyncCells();
    }

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        if (_cellTemplate == null)
        {
            Debug.LogError($"{nameof(MapGenerator)}: {nameof(_cellTemplate)} is not assigned", this);
            return;
        }

        float stepX = _cellSize + _spacing;
        float stepZ = _cellSize + _spacing;

        SyncCells();

        float startX = -(_columns - 1) * stepX * 0.5f;
        float startZ = -(_rows - 1) * stepZ * 0.5f;
        float cellY = _cellTemplate.transform.localPosition.y;

        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                Vector3 localPosition = new Vector3(startX + column * stepX, cellY, startZ + row * stepZ);

                GameObject cellObject = _factory.Create(_cellTemplate.gameObject, transform.position, _cellTemplate.transform.rotation, transform);
                cellObject.name = $"{_cellTemplate.name}_{row}_{column}";
                cellObject.transform.localPosition = localPosition;
                cellObject.transform.localRotation = _cellTemplate.transform.localRotation;
                cellObject.transform.localScale = _cellTemplate.transform.localScale * _cellSize;

                if (_cells[row * _columns + column] && cellObject.TryGetComponent(out Cell cell))
                    cell.Lock();
            }
        }
    }

    private void SyncCells()
    {
        int count = _rows * _columns;

        _cells ??= new List<bool>(count);

        while (_cells.Count < count)
            _cells.Add(false);

        if (_cells.Count > count)
            _cells.RemoveRange(count, _cells.Count - count);
    }
}
