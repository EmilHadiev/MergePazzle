using UnityEngine;

public class RobotSizer : MonoBehaviour
{
    [SerializeField] private RobotBody _robotBody;

    private const float ScaleStep = 0.1f;
    private const float MaxScale = 2f;

    private IRobot _robot;
    private IRobotData _data;
    private IRobotUpgrader _upgrader;

    private Transform Target => _robotBody.transform;

    private void OnValidate()
    {
        _robotBody ??= GetComponentInChildren<RobotBody>();    
    }

    private void Awake()
    {
        _robot = GetComponent<IRobot>();
        _upgrader = _robot.Upgrader;
    }

    private void Start()
    {
        _data = _robot.Data;
        UpdateScale();
    }

    private void OnEnable()
    {
        _upgrader.Upgraded += UpdateScale;
    }

    private void OnDisable()
    {
        _upgrader.Upgraded -= UpdateScale;
    }

    private void UpdateScale()
    {
        if (_data == null)
        {
            Debug.LogError($"{_data} is null");
            return;
        }

        float scale = Mathf.Min(1f + _data.RobotLevel * ScaleStep, MaxScale);

        Target.localScale = Vector3.one * scale;
    }
}
