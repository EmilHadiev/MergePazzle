using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(BulletHider))]
[RequireComponent(typeof(BulletAttackTrigger))]
[RequireComponent(typeof(BulletMover))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletAttackTrigger _attacker;
    [SerializeField] private ParticlePosition _particlePosition;

    public ParticlePosition ParticlePosition => _particlePosition;

    private void OnValidate()
    {
        _attacker ??= GetComponent<BulletAttackTrigger>();
        _particlePosition ??= GetComponentInChildren<ParticlePosition>();
    }

    /// <summary>
    /// Robot data
    /// </summary>
    public void SetData(RobotData robotData)
    {
        _attacker.SetDamage(robotData);
    }
}
