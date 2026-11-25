using UnityEngine;

public class CanvasLifter : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _yStep = 0.15f;

    private IRobotUpgrader _upgrader;

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
    }

    private void Awake()
    {
        _upgrader = GetComponentInParent<IRobotUpgrader>();
    }

    private void OnEnable()
    {
        if (_upgrader == null)
            return;

        _upgrader.Upgraded += OnUpgraded;
    }

    private void OnDisable()
    {
        if (_upgrader == null)
            return;

        _upgrader.Upgraded -= OnUpgraded;
    }

    private void OnUpgraded()
    {
        Vector3 canvasPos = _canvas.transform.position;
        canvasPos.y += _yStep;
        _canvas.transform.position = canvasPos;
    }
}
