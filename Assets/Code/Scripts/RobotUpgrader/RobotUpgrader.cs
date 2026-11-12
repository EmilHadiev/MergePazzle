using System;
using UnityEngine;

public class RobotUpgrader : MonoBehaviour, IRobotUpgrader
{
    public event Action Upgraded;
    private IRobotData _data;

    private void Start()
    {
        _data = GetComponent<IRobot>().Data;
        LevelUp();
    }

    public void LevelUp()
    {
        _data.LevelUp();
        Upgraded?.Invoke();
    }
}