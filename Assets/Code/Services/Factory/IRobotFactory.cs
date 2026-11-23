using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IRobotFactory
{
    UniTask<Robot> CreateRobot(RobotsName name, Vector3 position = default, Quaternion rotation = default);
}