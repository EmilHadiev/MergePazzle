public class RangeAttackLogic : IAttackLogic
{
    private readonly IBulletStorage _bulletSpawner;

    public RangeAttackLogic(IBulletStorage bulletSpawner)
    {
        _bulletSpawner = bulletSpawner;
    }

    public void Attack()
    {
        _bulletSpawner.Spawn();
    }
}