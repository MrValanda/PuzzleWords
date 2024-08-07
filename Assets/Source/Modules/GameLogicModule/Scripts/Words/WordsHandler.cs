using System.Collections.Generic;
using System.Linq;
using Source.Modules.GameLogicModule.Scripts.Levels;
using Source.Modules.SignalsModule.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Modules.GameLogicModule.Scripts.Words
{
    public class WordsHandler : MonoBehaviour
    {
        private readonly List<WordController> _guessedWordsControllers = new();
        private readonly List<Word> _guessedWords = new();
        
        [SerializeField] private Button _validateGame;
        
        private LevelData _levelData;
        private SignalBus _signalBus;
        private List<WordController> _wordControllers;

        public IReadOnlyCollection<Word> GuessesWords => _guessedWords.AsReadOnly();
        
        [Inject]
        private void Construct(LevelData levelData,SignalBus signalBus)
        {
            _levelData = levelData;
            _signalBus = signalBus;
        }
        
        public void Initialize(List<WordController> wordControllers)
        {
            _wordControllers = wordControllers;
            _wordControllers.ForEach(x=>x.WordCreated += OnWordCreated);
            _wordControllers.ForEach(x => x.WordChanged += OnWordChanged);
            _validateGame.onClick.AddListener(OnValidateGameClick);
        }

        private void OnDestroy()
        {
            _wordControllers.ForEach(x=>x.WordCreated -= OnWordCreated);
            _wordControllers.ForEach(x => x.WordChanged -= OnWordChanged);
        }

        private void OnValidateGameClick()
        {
            if (_guessedWordsControllers.Count != _levelData.Words.Count) return;
            

            _signalBus.Fire<LvlCompleteSignal>();
        }
        
        private void OnWordChanged(WordController wordController)
        {
            if (_levelData.Words.Contains(wordController.GetCurrentWord())) return;
            
            if (_guessedWordsControllers.Contains(wordController))
            {
                _guessedWords.Remove(wordController.GetCurrentWord());
                _guessedWordsControllers.Remove(wordController);
            }
        }

        private void OnWordCreated(WordController wordController)
        {
            if (_levelData.Words.Contains(wordController.GetCurrentWord()))
            {
                _guessedWords.Add(wordController.GetCurrentWord());
                _guessedWordsControllers.Add(wordController);
            }
        }
    }
}
