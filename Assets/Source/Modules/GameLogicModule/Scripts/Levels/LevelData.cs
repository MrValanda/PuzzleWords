using System.Collections.Generic;
using Source.Modules.GameLogicModule.Scripts.Words;

namespace Source.Modules.GameLogicModule.Scripts.Levels
{
    public class LevelData
    {
        private List<Word> _words = new List<Word>();
        public IReadOnlyCollection<Word> Words => _words.AsReadOnly();
        public void SetWords(List<Word> words)
        {
            _words = words;
        }
   
    }
}