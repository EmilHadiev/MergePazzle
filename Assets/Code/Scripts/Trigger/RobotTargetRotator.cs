using System.Linq;
using UnityEngine;

public class RobotTargetRotator : MonoBehaviour
{
    [SerializeField] private AttackSensor _sensor;
    [SerializeField] private BulletSpawnPosition[] _bulletspawnPositions;

    private Transform _target;
    private Vector3 _robotDefaultVector;
    private Quaternion[] _bulletDefaultRotations;

    private void OnValidate()
    {
        _sensor ??= GetComponentInChildren<AttackSensor>();
        _bulletspawnPositions ??= GetComponentsInChildren<BulletSpawnPosition>();
    }

    private void OnEnable()
    {
        _sensor.TargetDetected += OnTargetDetected;
        _sensor.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _sensor.TargetDetected -= OnTargetDetected;
        _sensor.TargetLost -= OnTargetLost;
    }

    private void Start()
    {
        _robotDefaultVector = transform.forward;
        _bulletDefaultRotations = _bulletspawnPositions
            .Select(v => v.transform.localRotation)
            .ToArray();
    }

    private void Update()
    {
        if (_target == null)
            return;

        RobotLookAt(_target.position);
        BulletsLookAt(_target.position);
    }

    private void RobotLookAt(Vector3 position)
    {
        var direction = position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.0001f)
            return;

        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void BulletsLookAt(Vector3 position)
    {
        foreach (var v in _bulletspawnPositions)
            v.transform.LookAt(position);
    }

    private void OnTargetDetected(Collider collider)
    {
        _target = collider.transform;
        Debug.Log(_target + " finded!!!");
    }

    private void OnTargetLost(Collider collider)
    {
        _target = null;

        transform.forward = _robotDefaultVector;
        ResetBulletRotations();
    }

    private void ResetBulletRotations()
    {
        for (int i = 0; i < _bulletspawnPositions.Length; i++)
            _bulletspawnPositions[i].transform.localRotation = _bulletDefaultRotations[i];
    }
}