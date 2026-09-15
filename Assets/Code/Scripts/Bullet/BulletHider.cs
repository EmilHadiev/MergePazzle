using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class BulletHider : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private int _lifeTime = 5;

    private CancellationTokenSource _cts;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnEntered;

        HideWithDelay().Forget();
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void OnEntered(Collider collider)
    {
        Hide();
    }

    private async UniTask HideWithDelay()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime), cancellationToken: _cts.Token);
        Hide();
        Debug.Log("Done");
    }

    private void Hide() => gameObject.SetActive(false);
}