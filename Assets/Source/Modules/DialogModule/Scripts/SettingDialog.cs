using Source.Modules.GameLogicModule.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Modules.DialogModule.Scripts
{
    public class SettingDialog : Dialog
    {
        [SerializeField] private Toggle _soundToggle;

        private SoundPlayer _soundPlayer;
        
        [Inject]
        private void Construct(SoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
        }
        
        protected override void Start()
        {
            base.Start();
            UpdateSoundState();
            _soundToggle.onValueChanged.AddListener(OnSoundToggleValueChanged);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _soundToggle.onValueChanged.RemoveListener(OnSoundToggleValueChanged);
        }

        private void OnSoundToggleValueChanged(bool arg0)
        {
            UpdateSoundState();
        }

        private void UpdateSoundState()
        {
            if (_soundToggle.isOn)
            {
                _soundPlayer.ActiveSound();
            }
            else
            {
                _soundPlayer.DeActiveSound();
            }
        }
    } 
}