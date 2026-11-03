using UnityEngine;

public class BulletAttackTrigger : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    private IRobotData _data;

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
        _observer.UnLock();
    }

    public void SetDamage(IRobotData robotData)
    {
        _data = robotData;
    }

    private void OnEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(_data.StartDamage);            
        }

        if (collider.TryGetComponent(out IDamageImpactView impactView))
        {
            impactView.Play(collider.ClosestPoint(transform.position));
        }
    }
}