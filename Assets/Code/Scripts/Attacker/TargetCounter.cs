using UnityEngine;

public class TargetCounter : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] public int _targetCount;

    public int TargetCount => _targetCount;
}
