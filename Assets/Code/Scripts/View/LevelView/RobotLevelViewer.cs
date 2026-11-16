using TMPro;
using UnityEngine;

public class RobotLevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _robotLevelText;

    private IRobotUpgrader _upgrader;
    private IRobotData _data;

    private void Awake()
    {
        _upgrader = GetComponentInParent<IRobotUpgrader>();
    }

    private void Start()
    {
        _data = GetComponentInParent<IRobot>().Data;
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
        _robotLevelText.text = $"{_data.RobotLevel}";
    }
}