using System;
using UnityEngine;
using UnityEngine.UI;

public class RobotUpgrader : MonoBehaviour, IRobotUpgrader
{
    public event Action Upgraded;
    private IRobotData _data;

    private void Start()
    {
        _data = GetComponent<IRobot>().Data;
    }

    public void LevelUp()
    {
        _data.LevelUp();
        Upgraded?.Invoke();
    }
}