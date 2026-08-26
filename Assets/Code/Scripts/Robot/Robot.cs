using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(RobotMover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(RobotHealth))]
public class Robot : MonoBehaviour, IRobot
{
    [SerializeField] private CharacterAnimator _animator;
    public ICharacterAnimator Animator => _animator;

    private void OnValidate()
    {
        _animator ??= GetComponent<CharacterAnimator>();
    }
}