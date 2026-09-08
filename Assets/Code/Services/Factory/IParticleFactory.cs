using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IParticleFactory
{
    ParticleView CreateParticle(GameObject prefab, ParticlePosition position);
    UniTask<ParticleView> CreateAsyncParticle(string assetAdrress, ParticlePosition position);
}