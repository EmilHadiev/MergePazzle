using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesCache<T> : IDisposable, IAddressablesCache<T> where T : UnityEngine.Object
{
    private readonly Dictionary<string, AsyncOperationHandle<T>> _cache;
    private CancellationTokenSource _cts;

    public AddressablesCache()
    {
        _cache = new Dictionary<string, AsyncOperationHandle<T>>(20);
        _cts = new CancellationTokenSource();
    }

    public async UniTask<T> LoadAssetAsync(string assetKey)
    {
        try
        {
            if (_cache.TryGetValue(assetKey, out var result))
            {
                return await result.ToUniTask(cancellationToken: _cts.Token);
            }

            var handle = Addressables.LoadAssetAsync<T>(assetKey);
            _cache[assetKey] = handle;

            return await _cache[assetKey].ToUniTask(cancellationToken: _cts.Token);
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            throw;
        }
    }

    public void Release(string assetKey)
    {
        if (_cache.TryGetValue(assetKey, out var result))
        {
            Addressables.Release(result);
            _cache.Remove(assetKey);
        }
        else
        {
            throw new System.ArgumentException(nameof(assetKey));
        }
    }

    public void ReleaseAll()
    {
        foreach (var key in _cache)
        {
            Addressables.Release(key.Value);
        }

        _cache.Clear();
    }

    public void CancelLoading()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();

        ReleaseAll();
    }
}