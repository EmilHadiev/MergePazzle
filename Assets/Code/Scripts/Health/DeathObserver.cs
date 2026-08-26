using UnityEngine;

public class DeathObserver : MonoBehaviour
{
    private IHealth _health;
    private ICharacterAnimator _animator;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _animator = GetComponent<ICharacterAnimator>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        _animator.DieTrigger();
    }

    private void Died()
    {
        gameObject.SetActive(false);
    }
}