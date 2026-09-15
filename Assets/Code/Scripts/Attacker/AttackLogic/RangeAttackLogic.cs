using Cysharp.Threading.Tasks;

public class RangeAttackLogic : IAttackLogic
{
    private readonly ParticlePosition[] _positions;
    private readonly IParticleFactory _factory;
    private readonly ParticleNames _particleName;
    private readonly IAttackLogic _bulletSpawner;

    private ParticleView[] _particles;

    public RangeAttackLogic(ParticleNames names, ParticlePosition[] particlePositions, IParticleFactory factory, IAttackLogic bulletSpawner)
    {
        _factory = factory;
        _particleName = names;
        _positions = particlePositions;
        _bulletSpawner = bulletSpawner;

        //CreateParticles().Forget();
    }

    public void Attack()
    {
        _bulletSpawner.Attack();
    }

    private async UniTask CreateParticles()
    {
        _particles = new ParticleView[_positions.Length];

        for (int i = 0; i < _particles.Length; i++)
        {
            var position = _positions[i];

            _particles[i] = await _factory.CreateAsyncParticle(_particleName.ToString(), position);
        }
    }
}