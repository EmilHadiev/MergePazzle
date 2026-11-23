using System;

public interface ICellDetector
{
    event Action<Cell> CellSelected;

    void StartWorking();
    void StopWorking();
}