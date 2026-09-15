using UnityEngine;

public class BulletAttackTrigger : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    private float _damage;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
    }

    public void SetDamage(RobotData robotData)
    {
        _damage = robotData.StartDamage;
    }

    private void OnEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
            health.TakeDamage(_damage);
    }
}