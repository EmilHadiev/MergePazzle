public interface ICharacterAnimator
{
    void StartAttacking();
    void StartRunning();

    void StopAttacking();
    void StopRunning();

    void Idle();

    void TakeDamageTrigger();

    void DieTrigger();
}