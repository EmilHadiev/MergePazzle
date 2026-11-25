using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    private Canvas _canvas;
    private Camera _camera;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        _camera = Camera.main;

        if (_camera == null)
        {
            Debug.LogError($"{name}: Camera.main не найдена Ч канвас не будет поворачиватьс€ и получать клики", this);
            enabled = false;
            return;
        }

        _canvas.worldCamera = _camera;
    }

    private void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
