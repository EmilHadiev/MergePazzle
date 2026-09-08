using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FighterEffectViewer : MonoBehaviour, ICharacterEffect
{
    [SerializeField] private ParticlePosition _electroFieldposition;

    [Inject] private IParticleFactory _factory;

    private ParticleView _particle;
    private IAttackable _attackable;

    private void Start()
    {
        CreateParticle().Forget();
        _attackable ??= GetComponent<IAttackable>();

        _attackable.AttackStarting += Play;
        _attackable.AttackEnding += Stop;
    }

    private void OnDestroy()
    {
        _attackable.AttackStarting -= Play;
        _attackable.AttackEnding -= Stop;
    }

    private async UniTask CreateParticle()
    {
        _particle = await _factory.CreateAsyncParticle(ParticleNames.ElectroField.ToString(), _electroFieldposition);
        _particle.Hide();
    }

    public void Play()
    {
        _particle.Show();
    }

    public void Stop()
    {
        _particle.Hide();
    }
}