using Source.Modules.DialogModule.Scripts;
using Source.Modules.GameLogicModule.Scripts;
using Source.Modules.SignalsModule.Scripts;
using Zenject;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class EndGameState : IState
    {
        private readonly DialogService _dialogService;
        private readonly SignalBus _signalBus;
        private StateMachine _stateMachine;
        
        public EndGameState(DialogService dialogService,SignalBus signalBus)
        {
            _dialogService = dialogService;
            _signalBus = signalBus;
        }
        
        public void Enter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _signalBus.Subscribe<MoveToMainMenuSignal>(OnMoveToMainMenuSignal);
            _dialogService.ShowDialog<EndGameDialog>();
        }

        public void Exit()
        {
            _signalBus.Unsubscribe<MoveToMainMenuSignal>(OnMoveToMainMenuSignal);
        }

        private void OnMoveToMainMenuSignal()
        {
            _stateMachine.SwitchState<MainMenuState>();
        }
    }
}