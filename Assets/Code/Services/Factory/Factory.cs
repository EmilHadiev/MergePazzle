using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Factory : IFactory
{
    private readonly IInstantiator _instantiator;
    private readonly IAddressablesCache<UnityEngine.Object> _addressables;

    public Factory(IInstantiator instantiator, IAddressablesCache<UnityEngine.Object> addressables)
    {
        _instantiator = instantiator;
        _addressables = addressables;
    }

    public GameObject Create(GameObject obj, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        return _instantiator.InstantiatePrefab(obj, position, rotation, parent);
    }

    public async UniTask<T> LoadAssetAsync<T>(string name) where T : UnityEngine.Object
    {
        var result = await _addressables.LoadAssetAsync(name);
        return result as T;
    }
}
