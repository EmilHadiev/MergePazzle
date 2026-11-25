using UnityEngine;
using Zenject;

public class DeathObserver : MonoBehaviour
{
    [Inject] private readonly IRobotContainer _container;

    private IHealth _health;
    private ICharacterAnimator _animator;
    private IRobot _robot;

    private void Awake()
    {
        _robot = GetComponent<IRobot>();

        _health = GetComponent<IHealth>();
        _animator = _robot.Animator;
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
        _container.Remove(_robot);
        _animator.DieTrigger();
    }

    private void Died()
    {
        gameObject.SetActive(false);
    }
}