using Cysharp.Threading.Tasks;
using UnityEngine;

public class ElectroXAttackEffectViewer : DefaultAttackEffectViewer
{
    [SerializeField] private ParticlePosition _secondAttackParticlePosition;
    [SerializeField] private ParticleNames _secondParticleName;

    private ParticleView _secondParticle;

    protected override async UniTask CreateParticle()
    {
        await base.CreateParticle();
        _secondParticle = await Factory.CreateAsyncParticle(_secondParticleName.ToString(), _secondAttackParticlePosition);
        _secondParticle?.Hide();
    }

    protected override void PlayAnimation()
    {
        base.PlayAnimation();
        _secondParticle.Show();
    }
}