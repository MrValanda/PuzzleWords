using UnityEngine;
using Zenject;

namespace Source.Modules.DialogModule.Scripts
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _dialogCanvasPrefab;
        [SerializeField] private string _dialogsPath;
        private DialogService _dialogService;
        public override void InstallBindings()
        {
            Canvas dialogCanvas = Instantiate(_dialogCanvasPrefab,null);
            DontDestroyOnLoad(dialogCanvas.gameObject);
            _dialogService = new DialogService(dialogCanvas.transform, _dialogsPath);
            Container.Bind<DialogService>().FromInstance(_dialogService);
        }
    }
}
