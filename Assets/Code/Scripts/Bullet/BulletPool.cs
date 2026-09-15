using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletPool : MonoBehaviour, IAttackLogic
{
    [SerializeField] private BulletNames _bulletName;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private BulletSpawnPosition[] _spawnPositions;
    [SerializeField] private int _poolSize;

    [Inject] private readonly IParticleFactory _factory;

    private List<Bullet> _bullets;
    private RobotData _robotData;

    private void Start()
    {
        _bullets = new List<Bullet>(_poolSize);
        _robotData = GetComponent<IRobot>().Data;
        CreateBullets().Forget();
    }

    private async UniTask CreateBullets()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            var bullet = Instantiate(_bulletTemplate);
            var particle = await _factory.CreateAsyncParticle(_bulletName.ToString(), bullet.ParticlePosition);
            _bullets.Add(bullet);

            bullet.SetData(_robotData);
            bullet.gameObject.SetActive(false);
        }
    }

    public void Attack()
    {
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            var position = _spawnPositions[i].transform;

            if (TryGetAvailableBullet(out Bullet bullet))
            {
                bullet.transform.SetLocalPositionAndRotation(position.position, position.rotation);
                bullet.gameObject.SetActive(true);
            }
            else
            {
                CreateBullets().Forget();
            }            
        }
    }

    private bool TryGetAvailableBullet(out Bullet bullet)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (_bullets[i].gameObject.activeInHierarchy == false)
            {
                bullet = _bullets[i];
                return true;
            }
        }

        bullet = null;
        return false;
    }
}