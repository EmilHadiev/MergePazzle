using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(RobotMover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(RobotAttackPerformer))]
[RequireComponent(typeof(RobotHealth))]
[RequireComponent(typeof(RobotSoundObserver))]
public class Robot : MonoBehaviour, IRobot
{
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private RobotMover _mover;
    [SerializeField] private RobotAttackPerformer _attacker;
    [SerializeField] private RobotData _data;

    public ICharacterAnimator Animator => _animator;
    public RobotData Data { get; private set; }
    public IRobotBehaviourStateMachine StateMachine { get; private set; }

    private void OnValidate()
    {
        _animator ??= GetComponent<CharacterAnimator>();
        _attacker ??= GetComponent<RobotAttackPerformer>();
        _mover ??= GetComponent<RobotMover>();
    }

    private void Awake()
    {
        Data = Instantiate(_data);
        StateMachine = new RobotBehaviourStateMachine(_attacker, _mover);
    }

    private void Start()
    {
        StateMachine.SwitchTo<StubState>();
    }
}