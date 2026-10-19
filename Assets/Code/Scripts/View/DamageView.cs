using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasRotator))]
public class DamageView : MonoBehaviour
{
    [SerializeField] private ParticlePosition _position;
    [SerializeField] private ParticleNames _particleName = ParticleNames.DamageParticle;

    [Inject] private IParticleFactory _particleFactory;

    private IHealth _health;
    private ParticleTextView _viewText;

    private void OnValidate()
    {
        _position ??= GetComponentInChildren<ParticlePosition>();
    }

    private void Awake()
    {
        _health = GetComponentInParent<IHealth>();
    }

    private void OnEnable()
    {
        _health.DamageTaked += OnDamageTaked;
    }

    private void OnDisable()
    {
        _health.DamageTaked -= OnDamageTaked;
    }

    private void Start()
    {
        CreateParticle().Forget();
    }

    private void OnDamageTaked(float damage)
    {
        _viewText.ShowText(damage.ToString());
        _viewText.Show();
    }

    private async UniTaskVoid CreateParticle()
    {
        try
        {
            var prefab = await _particleFactory.CreateAsyncParticle(_particleName.ToString(), _position);
            _viewText = prefab.GetComponent<ParticleTextView>();
            _viewText.Hide();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
