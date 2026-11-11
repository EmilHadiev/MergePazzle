using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(RobotMover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(RobotAttackPerformer))]
[RequireComponent(typeof(RobotHealth))]
[RequireComponent(typeof(RobotSoundObserver))]
[RequireComponent(typeof(RobotTargetRotator))]
[RequireComponent(typeof(RobotUpgrader))]
public class Robot : MonoBehaviour, IRobot
{
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private RobotMover _mover;
    [SerializeField] private RobotAttackPerformer _attacker;
    [SerializeField] private RobotsName _robotName;
    [SerializeField] private RobotUpgrader _upgrader;

    private RobotData _data;

    public ICharacterAnimator Animator => _animator;
    public IRobotData Data => _data;
    public IRobotBehaviourStateMachine StateMachine { get; private set; }

    public IRobotUpgrader Upgrader => _upgrader;

    private void OnValidate()
    {
        _animator ??= GetComponent<CharacterAnimator>();
        _attacker ??= GetComponent<RobotAttackPerformer>();
        _mover ??= GetComponent<RobotMover>();
        _upgrader ??= GetComponent<RobotUpgrader>();
    }

    private void Awake()
    {
        StateMachine = new RobotBehaviourStateMachine(_attacker, _mover);
    }

    private void Start()
    {
        StateMachine.SwitchTo<StubState>();
    }

    [Inject]
    public void Construct(IEnumerable<RobotData> data)
    {
        _data = data.First(n => n.RobotName == _robotName);
    }
}