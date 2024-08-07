using System.Collections.Generic;
using Source.Modules.GameLogicModule.Scripts.Words;
using UnityEngine;

namespace Source.Modules.GameLogicModule.Scripts.Clusters
{
    public class ClusterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _dragParent;
        [SerializeField] private ClusterCompositeRoot _clusterCompositeRoot;

        public Transform ClustersContainerParent => _parent;
        public Transform DraggedClustersParent => _dragParent;

        public void SpawnClusters(IEnumerable<Word> words)
        {
            foreach (Word levelDataWord in words.ShuffledCopy())
            {
                foreach (Cluster<char> wordCluster in levelDataWord.WordClusters.ShuffledCopy())
                {
                    ClusterCompositeRoot clusterCompositeRoot = Instantiate(_clusterCompositeRoot, ClustersContainerParent);
                    clusterCompositeRoot.Init(wordCluster);
                }
            }
        }
    }
}
