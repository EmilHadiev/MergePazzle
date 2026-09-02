using UnityEngine;

[CreateAssetMenu(menuName = "Data/" + nameof(RobotData), fileName = nameof(RobotData))]
public class RobotData : ScriptableObject
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
    public int RobotLevel = 1;
}