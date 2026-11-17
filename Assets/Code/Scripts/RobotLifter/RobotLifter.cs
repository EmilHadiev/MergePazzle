using UnityEngine;

public class RobotLifter : MonoBehaviour
{
    [SerializeField] private float _yStep = 0.05f;

    private IRobotUpgrader _upgrader;

    private void Awake()
    {
        _upgrader = GetComponentInParent<IRobotUpgrader>();
    }

    private void OnEnable()
    {
        _upgrader.Upgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        _upgrader.Upgraded -= OnUpgraded;
    }

    private void OnUpgraded()
    {
        Vector3 pos = transform.position;
        pos.y += _yStep;
        transform.position = pos;
    }
}
