using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DefaultAttackEffectViewer : MonoBehaviour, ICharacterEffect
{
    [SerializeField] private ParticlePosition _attackParticlePosition;
    [SerializeField] private ParticleNames _particleName;

    [Inject] protected readonly IParticleFactory Factory;

    private ParticleView _particle;
    private IAttackable _attackable;

    private void Start()
    {
        CreateParticle().Forget();
        _attackable ??= GetComponent<IAttackable>();

        _attackable.AttackEnding += Play;
    }

    private void OnDestroy()
    {
        _attackable.AttackEnding -= Play;
    }

    protected virtual async UniTask CreateParticle()
    {
        _particle = await Factory.CreateAsyncParticle(_particleName.ToString(), _attackParticlePosition);
        _particle.Hide();
    }

    public virtual void Play()
    {
        PlayAnimation();
    }

    public virtual void Stop()
    {

    }

    protected virtual void PlayAnimation()
    {
        _particle.Show();
    }
}