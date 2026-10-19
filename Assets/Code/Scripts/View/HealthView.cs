using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRotator))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private CanvasRotator _rotator;
    [SerializeField] private Image _healthBar;

    private IHealth _health;

    private void OnValidate()
    {
        _rotator ??= GetComponent<CanvasRotator>();
        _healthBar ??= GetComponentInChildren<Image>();
    }

    private void Awake()
    {
        _health = GetComponentInParent<IHealth>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealhChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealhChanged;
    }

    private void OnHealhChanged(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }
}