using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class LoadingState : IState
    {
        private const int NextLvlIndex = 1;
        private StateMachine _stateMachine;
        private AsyncOperation _asyncOperation;
        
        public void Enter(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            SceneManager.LoadSceneAsync(NextLvlIndex).completed += OnLoadedScene;
        }

        private void OnLoadedScene(AsyncOperation obj)
        {
            obj.completed -= OnLoadedScene;
            _stateMachine.SwitchState<PlayState>();
        }
    }
}