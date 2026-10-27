using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DamageImpactView : MonoBehaviour, IDamageImpactView
{
    [SerializeField] private ParticleNames _damagImpactParticle;
    [SerializeField] private ParticlePosition _damageParticlePosition;

    [Inject] private readonly IParticleFactory _particleFactory;

    private ParticleView _impact;

    private void Start()
    {
        CreateParticle().Forget();
    }

    public void Play(Vector3 takeDamagePosotion)
    {
        _impact.transform.position = takeDamagePosotion;
        _impact.Show();
    }


    private async UniTaskVoid CreateParticle()
    {
        _impact = await _particleFactory.CreateAsyncParticle(_damagImpactParticle.ToString(), _damageParticlePosition);
        _impact.Hide();
    }
}