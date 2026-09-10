using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DefaultAttackEffectViewer : MonoBehaviour, ICharacterEffect
{
    [SerializeField] private EffectViewInfo[] _effects;

    [Inject] private readonly IParticleFactory _factory;

    private ParticleView[] _particles;
    private IAttackable _attackable;

    private void Start()
    {
        CreateParticles().Forget();
        _attackable ??= GetComponent<IAttackable>();

        _attackable.AttackEnding += Play;
    }

    private void OnDestroy()
    {
        _attackable.AttackEnding -= Play;
    }

    private async UniTask CreateParticles()
    {
        _particles = new ParticleView[_effects.Length];
        for (int i = 0; i < _particles.Length; i++)
        {
            var effect = _effects[i];
            _particles[i] = await _factory.CreateAsyncParticle(effect.Name.ToString(), effect.Position);
            _particles[i].Hide();
        }
    }

    public virtual void Play()
    {
        PlayEffects();
    }

    public virtual void Stop()
    {

    }

    protected virtual void PlayEffects()
    {
        foreach (var effect in _particles)
            effect.Show();
    }
}