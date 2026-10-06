using System;
using UnityEngine;

public class RangeAttackSensor : AttackSensor
{
    [SerializeField] private LayerMask _mask;

    private const int MaxTargets = 1;

    private RobotData _data;
    private RaycastHit[] _hits;

    private bool _isTargetFound;
    private float _time;

    public override event Action<Collider> TargetDetected;
    public override event Action<Collider> TargetLost;

    private void Start()
    {
        var robot = GetComponentInParent<IRobot>();
        _data = robot.Data;
        _hits = new RaycastHit[MaxTargets];
    }

    private void Update()
    {
        if (_time > Time.deltaTime)
        {
            _time = 0;
            SearchTarget();
        }
        else
        {
            _time += Time.deltaTime;
        }
    }

    private void SearchTarget()
    {
        if (_isTargetFound)
            return;

        Ray ray = new Ray(transform.position, transform.forward);
        int count = Physics.RaycastNonAlloc(ray, _hits, _data.AttackRange);

        if (count > 0)
        {
            foreach (var target in _hits)
            {
                OnTargetDetected(target.collider);
                Debug.Log(target.collider.gameObject.name);
            }
        }
    }

    protected override void OnTargetDetected(Collider target)
    {
        _isTargetFound = true;
        TargetDetected?.Invoke(target);
    }

    protected override void OnTargetLost(Collider target)
    {
        _isTargetFound = false;
        TargetLost?.Invoke(target);
    }

    private void OnDrawGizmos()
    {
        if (_isTargetFound == false)
            return;

        Vector3 start = transform.position;
        Vector3 direction = transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, start + direction * _data.AttackRange);
    }
}