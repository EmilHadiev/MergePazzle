using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();
        BindCellDetector();
        BindCombatSystem();
        BindRobotContainer();
    }

    private void BindCombatSystem()
    {
        Container.BindInterfacesTo<CombatSystem>().AsSingle();
    }

    private void BindRobotContainer()
    {
        Container.BindInterfacesTo<RobotContainer>().AsSingle();
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