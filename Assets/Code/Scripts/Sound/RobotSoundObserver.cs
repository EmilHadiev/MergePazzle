using UnityEngine;

public class RobotSoundObserver : MonoBehaviour
{
    private ISoundContainer _soundContainer;
    private IHealth _health;
    private IAttackable _attackable;


    private void Awake()
    {
        _soundContainer = GetComponentInChildren<ISoundContainer>();
        _health = GetComponent<IHealth>();
        _attackable = GetComponent<IAttackable>();
    }

    private void OnEnable()
    {
        _health.DamageTaked += OnDamageTaked;
        _attackable.AttackEnding += OnAttacked;
    }

    private void OnDisable()
    {
        _health.DamageTaked -= OnDamageTaked;
        _attackable.AttackEnding -= OnAttacked;
    }

    private void OnDamageTaked(float damage)
    {
        _soundContainer.Play(SoundNames.GetHit);
    }

    private void OnAttacked()
    {
        _soundContainer.Play(SoundNames.Attack);
    }
}