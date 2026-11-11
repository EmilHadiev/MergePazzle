using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/" + nameof(RobotData), fileName = nameof(RobotData))]
public class RobotData : ScriptableObject, IRobotData
{
    [field: SerializeField] public float StartHealth { get; private set; }
    [field: SerializeField] public float StartDamage { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; } = 3f;
    [field: SerializeField] public float AttackRadius { get; private set; } = 3f;
    [field: SerializeField] public float StartAttackSpeed { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float StartPrice { get; private set; }
    [field: SerializeField] public AttackType AttackType { get; private set; }
    [field: SerializeField] public LayerMask AttackTarget { get; private set; }
    [field: SerializeField] public RobotsName RobotName { get; private set; }
    [field: SerializeField] public int RobotLevel { get; private set; } = 1;

    private RobotStatsCalculator _statsCalculator;

    private void Awake()
    {
        _statsCalculator = new RobotStatsCalculator(this);
        Debug.Log($"Кальклуятор для {RobotName} создан!");
    }

    public void LevelUp()
    {
        RobotLevel++;
    }

    public float GetTotalHealth() => _statsCalculator.GetHealth();
    public float GetTotalDamage() => _statsCalculator.GetDamage();
    public float GetSellingPrice() => _statsCalculator.GetSellingPrice();
}