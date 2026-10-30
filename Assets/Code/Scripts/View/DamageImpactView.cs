using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DamageImpactView : MonoBehaviour, IDamageImpactView
{
    [SerializeField] private ParticleNames _damagImpactParticle = ParticleNames.TakeDamageImpact;
    [SerializeField] private ParticlePosition _damageParticlePosition;

    [SerializeField] private ParticleNames _criticalImpactParticle = ParticleNames.Smoke;
    [SerializeField] private ParticlePosition _criticalParticlePosition;

    private const float CriticalPercent = 0.25f;

    [Inject] private readonly IParticleFactory _particleFactory;

    private ParticleView _impact;
    private ParticleView _critical;
    private IHealth _health;

    private bool _isCriticalDamage;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        CreateParticle().Forget();
    }

    public void Play(Vector3 takeDamagePosotion)
    {
        _impact.transform.position = takeDamagePosotion;
        _impact.Show();
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if (_isCriticalDamage)
            return;

        _isCriticalDamage = (currentHealth / maxHealth) <= CriticalPercent;

        if (_isCriticalDamage)
            _critical.Show();
    }

    private async UniTaskVoid CreateParticle()
    {
        _impact = await _particleFactory.CreateAsyncParticle(_damagImpactParticle.ToString(), _damageParticlePosition);
        _impact.Hide();

        _critical = await _particleFactory.CreateAsyncParticle(_criticalImpactParticle.ToString(), _criticalParticlePosition);
        _critical.Hide();
    }
}