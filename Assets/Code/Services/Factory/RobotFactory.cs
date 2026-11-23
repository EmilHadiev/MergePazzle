using Cysharp.Threading.Tasks;
using UnityEngine;

public class RobotFactory : IRobotFactory
{
    private IFactory _factory;

    public RobotFactory(IFactory factory)
    {
        _factory = factory;
    }

    public async UniTask<Robot> CreateRobot(RobotsName name, Vector3 position = default, Quaternion rotation = default)
    {
        Robot robot = await _factory.LoadPrefabAsync<Robot>(name.ToString());
        robot.transform.SetPositionAndRotation(position, rotation);

        return robot;
    }
}
