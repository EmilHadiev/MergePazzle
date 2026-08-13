using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();
    }
    
    private void BindFactory()
    {
        Container.BindInterfacesTo<Factory>().AsSingle();
    }
}