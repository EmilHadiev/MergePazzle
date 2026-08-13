using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IFactory
{
    GameObject Create(GameObject obj, Vector3 position = default, Quaternion rotation = default, Transform parent = default);
    UniTask<T> LoadAssetAsync<T>(string name) where T : UnityEngine.Object;
}