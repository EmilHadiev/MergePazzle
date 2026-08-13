using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindAddressablesService();
    }

    private void BindAddressablesService()
    {
        Container.BindInterfacesTo<AddressablesCache<UnityEngine.Object>>().AsSingle();
    }
}