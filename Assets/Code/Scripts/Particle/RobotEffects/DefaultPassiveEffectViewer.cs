using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DefaultPassiveEffectViewer : MonoBehaviour, ICharacterEffect
{
    [SerializeField] private EffectViewInfo[] _effects;

    [Inject] private readonly IParticleFactory _factory;

    private ParticleView[] _particles;

    private void Start()
    {
        CreateParticles().Forget();
    }

    private async UniTask CreateParticles()
    {
        _particles = new ParticleView[_effects.Length];

        for (int i = 0; i < _particles.Length; i++)
        {
            var info = _effects[i];
            _particles[i] = await _factory.CreateAsyncParticle(info.Name.ToString(), info.Position);
            _particles[i].Show();
        }
    }

    public void Play()
    {
        
    }

    public void Stop()
    {

    }
}