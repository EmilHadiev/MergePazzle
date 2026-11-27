using UnityEngine;
using UnityEngine.UI;

public class RobotUpgraderView : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;

    private IRobotUpgrader _upgrader;

    private void Awake()
    {
        _upgrader = GetComponentInParent<IRobotUpgrader>();
    }

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(Upgrade);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(Upgrade);
    }

    private void Upgrade()
    {
        _upgrader.TryUpgrade();
    }
}