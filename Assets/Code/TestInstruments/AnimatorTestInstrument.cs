using UnityEngine;

public class AnimatorTestInstrument : MonoBehaviour
{
    [SerializeField] private CharacterAnimator[] _animators;

    private void Start()
    {
        Debug.Log($"0 - {nameof(Idle)}");
        Debug.Log($"1 - {nameof(Die)}");
        Debug.Log($"2 - {nameof(Attack)}");
        Debug.Log($"3 - {nameof(Running)}");
        Debug.Log($"4 - {nameof(TakeDamage)}");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0))
            Idle();

        if (Input.GetKeyUp(KeyCode.Alpha1))
            Die();

        if (Input.GetKeyUp(KeyCode.Alpha2))
            Attack();

        if (Input.GetKeyUp(KeyCode.Alpha3))
            Running();

        if (Input.GetKeyUp(KeyCode.Alpha4))
            TakeDamage();
    }

    private void Attack()
    {
        foreach (var anim in _animators)
        {
            anim.StopRunning();
            anim.StartAttacking();
        }
    }

    private void Running()
    {
        foreach (var anim in _animators)
        {
            anim.StopAttacking();
            anim.StartRunning();
        }
    }

    private void TakeDamage()
    {
        foreach (var anim in _animators)
        {
            anim.TakeDamageTrigger();
        }
    }

    private void Die()
    {
        foreach (var anim in _animators)
        {
            anim.DieTrigger();
        }
    }

    private void Idle()
    {
        foreach (var anim in _animators)
        {
            anim.Idle();
        }
    }
}
