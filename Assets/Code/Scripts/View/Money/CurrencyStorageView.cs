using TMPro;
using UnityEngine;

public abstract class CurrencyStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    protected ICurrency Currency;

    private void OnValidate()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Currency.Changed += OnChanged;
    }

    private void OnDisable()
    {
        Currency.Changed -= OnChanged;
    }

    private void OnChanged(int money)
    {
        _text.text = $"{money}";
    }
}