using System;
using UnityEngine;

public abstract class AttackSensor : MonoBehaviour
{
    public abstract event Action<Collider> TargetDetected;
    public abstract event Action<Collider> TargetLost;

    protected abstract void OnTargetDetected(Collider target);
    protected abstract void OnTargetLost(Collider target);
}