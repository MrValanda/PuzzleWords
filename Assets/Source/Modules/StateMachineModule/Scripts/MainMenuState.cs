using Source.Modules.DialogModule.Scripts;
using Source.Modules.GameLogicModule.Scripts;
using Source.Modules.SignalsModule.Scripts;
using Zenject;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class MainMenuState : IState
    {
        private readonly DialogService _dialogService;
        private readonly SignalBus _signalBus;
        private StateMachine _stateMachine;
        
        public MainMenuState(DialogService dialogService,SignalBus signalBus)
        {
            _dialogService = dialogService;
            _signalBus = signalBus;
        }

        public void Enter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _dialogService.ShowDialog<MainMenuDialog>();
            _signalBus.Subscribe<LvlStartSignal>(OnLvlStartSignal);
        }

        public void Exit()
        {
            _signalBus.Unsubscribe<LvlStartSignal>(OnLvlStartSignal);
        }

        private void OnLvlStartSignal()
        {
            _stateMachine.SwitchState<LoadingState>();
        }
    }
}