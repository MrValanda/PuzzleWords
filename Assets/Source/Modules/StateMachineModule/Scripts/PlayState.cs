using Source.Modules.SignalsModule.Scripts;
using UnityEngine;
using Zenject;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class PlayState : IState
    {
        private readonly SignalBus _signalBus;
        private StateMachine _stateMachine;
        
        [Inject]
        public PlayState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Enter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _signalBus.Subscribe<LvlCompleteSignal>(OnLvlCompleteSignal);
        }

        private void OnLvlCompleteSignal()
        {
            _signalBus.Unsubscribe<LvlCompleteSignal>(OnLvlCompleteSignal);
           _stateMachine.SwitchState<EndGameState>();
        }
    }
}