using UnityEngine;

[RequireComponent(typeof(CanvasRotator))]
[RequireComponent(typeof(CanvasLifter))]
[RequireComponent(typeof(RobotUpgraderView))]
[RequireComponent(typeof(RobotLevelViewer))]
[RequireComponent(typeof(Canvas))]
public class RobotDeleterAndUpgraderView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void OnValidate()
    {
        _canvas ??= GetComponent<Canvas>();
    }

    public void Show()
    {
        _canvas.enabled = true;
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }    
}