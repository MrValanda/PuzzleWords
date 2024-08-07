using System.Collections.Generic;
using Source.Modules.GameLogicModule.Scripts.Words.WordCells;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Words
{
    public class WordCompositeRoot : MonoBehaviour
    {
        [SerializeField] private WordCellController _wordCellController;
        [SerializeField] private Transform _parent;
        [field:SerializeField] public WordController WordController { get; private set; }

        public void Composite(int countCells)
        {
            List<WordCellController> wordCellControllers = new List<WordCellController>(countCells);

            for (int i = 0; i < countCells; i++)
            {
                WordCellController wordCellController = Instantiate(_wordCellController, _parent);
                wordCellController.Initialize(WordController);
                wordCellControllers.Add(wordCellController);
            }

            WordController.Initialize(wordCellControllers);
        }
    }
}
