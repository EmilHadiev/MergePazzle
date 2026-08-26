using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
{
    [SerializeField] Animator _animator;

    private const string Running = nameof(Running);
    private const string Attacking = nameof(Attacking);
    private const string TakeDamage = nameof(TakeDamage);
    private const string Die = nameof(Die);

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void StartRunning() => _animator.SetBool(Running, true);
    public void StopRunning() => _animator.SetBool(Running, false);

    public void StartAttacking() => _animator.SetBool(Attacking, true);
    public void StopAttacking() => _animator.SetBool(Attacking, false);

    public void TakeDamageTrigger() => _animator.SetTrigger(TakeDamage);

    public void DieTrigger() => _animator.SetTrigger(Die);

    public void Idle()
    {
        _animator.ResetTrigger(TakeDamage);
        _animator.ResetTrigger(Die);
        StopRunning();
        StopAttacking();
    }
}
