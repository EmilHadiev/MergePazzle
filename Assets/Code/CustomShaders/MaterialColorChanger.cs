using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private string _colorProperty = "_Color";

    private MaterialPropertyBlock _block;

    private void Awake()
    {
        Initialize();
    }

    private void OnValidate()
    {
        if (_renderers == null || _renderers.Length == 0)
            _renderers = GetComponentsInChildren<Renderer>();
    }

    public void ChangeColor(Color color)
    {
        Initialize();

        int propertyID = Shader.PropertyToID(_colorProperty);

        for (int i = 0; i < _renderers.Length; i++)
        {
            Renderer renderer = _renderers[i];

            if (renderer == null)
                continue;

            renderer.GetPropertyBlock(_block);
            _block.SetColor(propertyID, color);
            renderer.SetPropertyBlock(_block);
        }
    }

    private void Initialize()
    {
        _block ??= new MaterialPropertyBlock();

        if (_renderers == null || _renderers.Length == 0)
            _renderers = GetComponentsInChildren<Renderer>();
    }
}
