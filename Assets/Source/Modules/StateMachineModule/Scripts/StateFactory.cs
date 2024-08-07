using Zenject;

namespace Source.Modules.StateMachineModule.Scripts
{
    public class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) =>
            _container = container;

        public T CreateState<T>() where T : IState =>
            _container.Resolve<T>();
    }
}