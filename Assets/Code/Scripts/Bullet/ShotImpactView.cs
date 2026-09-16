using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ShotImpactView : MonoBehaviour
{
    [SerializeField] private ParticleNames _shootParticle = ParticleNames.ShotImpact;
    [SerializeField] private ParticlePosition[] _shootPositions;

    [Inject] private readonly IParticleFactory _factory;

    private ParticleView[] _views;
    private IAttackable _attackable;

    private void Awake()
    {
        _attackable = GetComponent<IAttackable>();
    }

    private void OnEnable()
    {
        _attackable.AttackStarting += PlayView;
    }

    private void OnDisable()
    {
        _attackable.AttackStarting -= PlayView;
    }

    private void Start()
    {
        _views = new ParticleView[_shootPositions.Length];
        CreateParticle().Forget();
    }

    private async UniTask CreateParticle()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            var particle = await _factory.CreateAsyncParticle(_shootParticle.ToString(), _shootPositions[i]);
            particle.Hide();
            _views[i] = particle;
        }
    }

    public void PlayView()
    {
        foreach (var particle in _views)
            particle.Show();
    }
}
