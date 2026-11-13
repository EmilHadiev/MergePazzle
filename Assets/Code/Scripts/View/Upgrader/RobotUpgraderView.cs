using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRotator))]
[RequireComponent(typeof(RobotDeleterView))]
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
        _upgrader.Upgrade();
        Debug.Log("Click!!!");
    }
}