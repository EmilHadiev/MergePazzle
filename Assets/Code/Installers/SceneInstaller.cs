using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();
        BindCellDetector();
    }
    
    private void BindFactory()
    {
        Container.BindInterfacesTo<Factory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ParticleFactory>().AsSingle();
        Container.BindInterfacesTo<RobotFactory>().AsSingle();
    }

    private void BindCellDetector()
    {
        Container.BindInterfacesTo<CellDetector>().AsSingle();
    }
}