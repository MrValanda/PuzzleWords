using Source.Modules.GameLogicModule.Scripts;
using Source.Modules.GameLogicModule.Scripts.Words;
using Source.Modules.SignalsModule.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Word = Source.Modules.GameLogicModule.Scripts.Words.Word;

namespace Source.Modules.DialogModule.Scripts
{
    public class EndGameDialog : Dialog
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private Button _moveToMainMenu;
        private WordsHandler _wordsHandler;
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(WordsHandler wordsHandler,SignalBus signalBus)
        {
            _wordsHandler = wordsHandler;
            _signalBus = signalBus;
        }
        
        public override void Show()
        {
            base.Show();
            foreach (Word wordsHandlerGuessesWord in _wordsHandler.GuessesWords)
            {
                _tmpText.text += wordsHandlerGuessesWord + "\n";
            }
            _moveToMainMenu.onClick.AddListener(OnMoveToMainMenuClick);
        }

        private void OnMoveToMainMenuClick()
        {
            Hide();
            _signalBus.TryFire<MoveToMainMenuSignal>();
        }
    }
}