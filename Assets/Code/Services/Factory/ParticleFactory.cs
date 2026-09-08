using Cysharp.Threading.Tasks;
using UnityEngine;

public class ParticleFactory : IParticleFactory
{
    private readonly IFactory _factory;

    public ParticleFactory(IFactory factory)
    {
        _factory = factory;
    }

    public async UniTask<ParticleView> CreateAsyncParticle(string assetAdrress, ParticlePosition position)
    {
        var handle = _factory.LoadPrefabAsync<ParticleView>(assetAdrress);

        var particle = await handle;

        particle.transform.SetPositionAndRotation(position.transform.position, position.transform.rotation);
        particle.transform.parent = position.transform;
        particle.transform.localScale = position.transform.lossyScale;

        return particle.GetComponent<ParticleView>();
    }

    public ParticleView CreateParticle(GameObject prefab, ParticlePosition position)
    {
        var particle = _factory.Create(prefab);
        prefab.transform.SetPositionAndRotation(position.transform.position, position.transform.rotation);
        prefab.transform.parent = position.transform;

        return particle.GetComponent<ParticleView>();
    }
}