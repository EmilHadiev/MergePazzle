using System;
using UnityEngine;

[RequireComponent(typeof(RobotSizer))]
public class RobotUpgrader : MonoBehaviour, IRobotUpgrader
{
    public event Action Upgraded;
    private IRobotData _data;

    private void Start()
    {
        _data = GetComponent<IRobot>().Data;
    }

    public void Upgrade()
    {
        LevelUp();
        Upgraded?.Invoke();
    }

    private void LevelUp()
    {
        _data.LevelUp();
    }
}