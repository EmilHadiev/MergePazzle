using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TriggerObserver : MonoBehaviour
{
    private static readonly Predicate<Collider> IsDestroyed = collider => collider == null;

    public event Action<Collider> Entered;
    public event Action<Collider> Exited;

    private readonly HashSet<Collider> _colliders = new();

    private void OnTriggerEnter(Collider other)
    {
        _colliders.RemoveWhere(IsDestroyed);

        if (_colliders.Add(other))
            Entered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_colliders.Remove(other))
            Exited?.Invoke(other);
    }

    private void OnDisable()
    {
        _colliders.Clear();
    }

    /// <summary>Сброс запомненных коллайдеров (совместимость со старым API, используется пулями).</summary>
    public void UnLock() => _colliders.Clear();
}
