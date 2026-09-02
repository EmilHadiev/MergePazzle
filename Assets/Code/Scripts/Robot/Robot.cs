using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(RobotMover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(RobotHealth))]
public class Robot : MonoBehaviour, IRobot
{
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private RobotData _data;

    public ICharacterAnimator Animator => _animator;
    public RobotData Data { get; private set; }

    private void OnValidate()
    {
        _animator ??= GetComponent<CharacterAnimator>();
    }

    private void Awake()
    {
        Data = Instantiate(_data);
    }
}