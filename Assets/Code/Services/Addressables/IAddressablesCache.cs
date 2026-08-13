using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAddressablesCache<T> where T : Object
{
    UniTask<T> LoadAssetAsync(string assetKey);
    void Release(string assetKey);
    void ReleaseAll();
    void CancelLoading();
}