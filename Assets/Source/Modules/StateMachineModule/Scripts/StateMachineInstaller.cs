using Zenject;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StateFactory>().AsSingle().NonLazy();
            Container.Bind<LoadingState>().AsSingle().NonLazy();
            Container.Bind<PlayState>().AsSingle().NonLazy();
            Container.Bind<EndGameState>().AsSingle().NonLazy();
            Container.Bind<MainMenuState>().AsSingle().NonLazy();
        }
    }
}