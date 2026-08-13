using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Creator : MonoBehaviour
{
    [SerializeField] private Vector3 _position;

    [Inject] private IFactory _factory;

    private void Start()
    {
        CreateAsync().Forget();
    }

    private async UniTask CreateAsync()
    {
        var result = await _factory.LoadAssetAsync<GameObject>("Hermit Robot Blue");
        _factory.Create(result, position: _position);
    }
}
