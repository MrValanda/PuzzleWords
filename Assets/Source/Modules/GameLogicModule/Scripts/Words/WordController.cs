using System;
using System.Collections.Generic;
using System.Linq;
using Source.Modules.GameLogicModule.Scripts.Clusters;
using Source.Modules.GameLogicModule.Scripts.Words.WordCells;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Words
{
    public class WordController : MonoBehaviour
    {
        public event Action<WordController> WordCreated;
        public event Action<WordController> WordChanged;
        private readonly Dictionary<Cluster<char>, List<int>> _clusters = new();
        
        [SerializeField] private WordView _wordView;
        private List<WordCellController> _wordCellControllers;
        
        private WordModel _wordModel;
        
        public void Initialize(List<WordCellController> wordCellControllers)
        {
            _wordModel = new WordModel();
            _wordCellControllers = wordCellControllers;
            for (var i = 0; i < _wordCellControllers.Count; i++)
            {
                _wordCellControllers[i].SetCellIndex(i);
            }
        }

        public Word GetCurrentWord() => _wordModel.CurrentWord;
        
        public void AnimateWordCells(Cluster<char> cluster, int startIndex)
        {
            for (var i = 0; i < cluster.ClusterItems.Count; i++) _wordCellControllers[i + startIndex].StartAnimate();
        }

        public void StopAnimateCells()
        {
            foreach (WordCellController wordCellController in _wordCellControllers.Where(x => x.IsEmptyCell()))
                wordCellController.StopAnimate();
        }

        public bool CanAddCluster(Cluster<char> cluster, int index)
        {
            if (index + cluster.ClusterItems.Count > _wordCellControllers.Count) return false;
            
            for (var i = 0; i < cluster.ClusterItems.Count; i++)
            {
                if (_wordCellControllers[i + index].IsEmptyCell() == false) return false;
            }

            return true;
        }

        public void AddCluster(Cluster<char> cluster, int index)
        {
            if (CanAddCluster(cluster, index) == false) return;

            if (_clusters.ContainsKey(cluster)) RemoveCluster(cluster);

            List<int> clustersIndex = new(cluster.ClusterItems.Count);
            for (int i = index, j = 0; j < cluster.ClusterItems.Count; i++, j++)
            {
                _wordCellControllers[i].UpdateCellItem(cluster.ClusterItems[j]);
                clustersIndex.Add(i);
            }

            _clusters.Add(cluster, clustersIndex);
            RegenerateCurrentWord();
            
            if (_wordModel.CurrentWord.Count == _wordCellControllers.Count)
            {
                WordCreated?.Invoke(this);
            }
        }

        public void RemoveCluster(Cluster<char> cluster)
        {
            for (var i = 0; i < cluster.ClusterItems.Count; i++)
                _wordCellControllers[_clusters[cluster][i]].UpdateCellItem(null);
            
            _clusters.Remove(cluster);
            
            RegenerateCurrentWord();
        }

        private void RegenerateCurrentWord()
        {
            _wordModel.CurrentWord.WordClusters.Clear();
            foreach (var keyValuePair in _clusters.OrderBy(x => x.Value[0]))
                _wordModel.CurrentWord.AddCluster(keyValuePair.Key);
            WordChanged?.Invoke(this);
        }
    }
}