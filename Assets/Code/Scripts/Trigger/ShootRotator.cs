using System.Linq;
using UnityEngine;

public class ShootRotator : MonoBehaviour
{
    [SerializeField] private AttackSensor _sensor;
    [SerializeField] private BulletSpawnPosition[] _bulletspawnPositions;

    private Transform _target;
    private Vector3 _robotDefaulVector;
    private Vector3[] _bulletDefaultVectors;

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
        _robotDefaulVector = transform.forward;
        _bulletDefaultVectors = _bulletspawnPositions.Select(v => v.transform.forward).ToArray();
    }

    private void Update()
    {
        if (_target == null)
            return;

        transform.LookAt(_target.position);
        //BulletsLookAt(_target.position);
    }

    private void RobotLookAt(Vector3 position)
    {
        
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

        transform.forward = _robotDefaulVector;
        for (int i = 0; i < _bulletspawnPositions.Length; i++)
            _bulletspawnPositions[i].transform.LookAt(_bulletDefaultVectors[i]);
    }
}