using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private RobotData[] _robotsData;

    public override void InstallBindings()
    {
        BindAddressablesService();
        BindRobotsData();
    }

    private void BindRobotsData()
    {
        var robotsData = new List<RobotData>(_robotsData.Length);

        for (int i = 0; i < _robotsData.Length; i++)
        {
            var data = Instantiate(_robotsData[i]);
            robotsData.Add(data);
        }

        Container.Bind<IEnumerable<RobotData>>().FromInstance(robotsData).AsSingle();
    }

    private void BindAddressablesService()
    {
        Container.BindInterfacesTo<AddressablesCache<UnityEngine.Object>>().AsSingle();
    }
}