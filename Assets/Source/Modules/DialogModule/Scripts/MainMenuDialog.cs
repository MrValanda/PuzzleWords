using Source.Modules.SignalsModule.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Modules.DialogModule.Scripts
{
    public class MainMenuDialog : Dialog
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingButton;
        
        private SignalBus _signalBus;
        private DialogService _dialogService;
        
        [Inject]
        public void Construct(SignalBus signalBus,DialogService dialogService)
        {
            _signalBus = signalBus;
            _dialogService = dialogService;
        }

        protected override void Start()
        {
            base.Start();
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
            _settingButton.onClick.AddListener(OnSettingButtonClick);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _settingButton.onClick.RemoveListener(OnSettingButtonClick);
            _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
        }

        private void OnSettingButtonClick()
        {
            _dialogService.ShowDialog<SettingDialog>();
        }

        private void OnStartGameButtonClick()
        {
            _signalBus.TryFire<LvlStartSignal>();
            Hide();
        }
    }
}