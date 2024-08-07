using Zenject;

namespace Source.Modules.SignalsModule.Scripts
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<LvlCompleteSignal>();
            Container.DeclareSignal<MoveToMainMenuSignal>();
            Container.DeclareSignal<LvlStartSignal>();
            SignalBusInstaller.Install(Container);
        }
    }
}
