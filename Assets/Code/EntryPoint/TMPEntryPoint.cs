using UnityEngine;
using Zenject;

public class TMPEntryPoint : MonoBehaviour
{
    [SerializeField] private int _startCoins;

    [Inject] private readonly ICoinsStorage _coinsStorage;

    private void Start()
    {
        _coinsStorage.Add(_startCoins);
    }
}