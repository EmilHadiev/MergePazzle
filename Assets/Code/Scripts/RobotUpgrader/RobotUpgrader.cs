using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(RobotSizer))]
public class RobotUpgrader : MonoBehaviour, IRobotUpgrader
{
    public event Action Upgraded;
    private IRobotData _data;

    [Inject] private readonly ICoinsStorage _coinsStorage;

    private void Start()
    {
        _data = GetComponent<IRobot>().Data;
    }

    public void TryUpgrade()
    {
        int price = Convert.ToInt32(_data.GetUpgradePrice());

        if (_coinsStorage.TrySpend(price))
        {
            LevelUp();
            Upgraded?.Invoke();
        }
    }

    private void LevelUp()
    {
        _data.LevelUp();
    }
}