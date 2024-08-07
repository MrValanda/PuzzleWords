using System.Collections.Generic;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Words
{
    public class WordsSpawner : MonoBehaviour
    {
        [SerializeField] private WordCompositeRoot _wordCompositeRoot;
        [SerializeField] private Transform _parent;
        
        public List<WordCompositeRoot> SpawnWords(IEnumerable<Word> words)
        {
            List<WordCompositeRoot> wordCompositeRoots = new(); 
            foreach (Word levelDataWord in words)
            {
                WordCompositeRoot wordCompositeRoot = Instantiate(_wordCompositeRoot, _parent);
                wordCompositeRoot.Composite(levelDataWord.Count);
                wordCompositeRoots.Add(wordCompositeRoot);
            }

            return wordCompositeRoots;
        }
    }
}
